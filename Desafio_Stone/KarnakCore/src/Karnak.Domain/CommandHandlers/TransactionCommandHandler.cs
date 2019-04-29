using Karnak.Domain.Commands;
using Karnak.Domain.Core.Bus;
using Karnak.Domain.Core.Notifications;
using Karnak.Domain.Events;
using Karnak.Domain.Interfaces;
using Karnak.Domain.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Karnak.Domain.CommandHandlers
{
    public class TransactionCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewTransactionCommand, bool>,
        IRequestHandler<UpdateTransactionCommand, bool>,
        IRequestHandler<RemoveTransactionCommand, bool>
    {
        private readonly ICardRepository _cardRepository;
        private readonly ITransactionRepository _transactionTypeRepository;
        private readonly ITransactionStatusRepository _transactionStatusRepository;

        private readonly IHostingEnvironment _env;

        private readonly IMediatorHandler Bus;

        public TransactionCommandHandler(
                                        ITransactionRepository transactionTypeRepository,
                                        ITransactionStatusRepository transactionStatusRepository,
                                        ICardRepository cardRepository,
                                      IHostingEnvironment env,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) :base(uow, bus, notifications)
        {
            _transactionTypeRepository = transactionTypeRepository;
            _transactionStatusRepository = transactionStatusRepository;
            _cardRepository = cardRepository;
            Bus = bus;
            _env = env;
        }

        public Task<bool> Handle(RegisterNewTransactionCommand message, CancellationToken cancellationToken)
        {
            // Validate message
            if (!message.IsValid()) { NotifyValidationErrors(message); return Task.FromResult(false); }

            // pegar as configuracoes
            var config = new ConfigurationBuilder().SetBasePath(_env.ContentRootPath).AddJsonFile("appsettings.json").Build();
            
            // pegar informacoes do cartao
            Card card = _cardRepository.GetById(message.IdCard);

            // cartao bloqueado
            if (card.Blocked == 0)
            {
                TransactionStatus statusNegada = GetItemStatus(config, "TransacaoNegada");
                var transactionNgada = new Transaction(message.Id, message.Amount, message.IdTransactionType, message.IdCard, statusNegada.Id, message.Number, DateTime.Now);
                _transactionTypeRepository.Add(transactionNgada);
                if (Commit())
                {
                    TransactionStatus status = GetItemStatus(config, "CartaoBloqueado");
                    Bus.RaiseEvent(new DomainNotification(message.MessageType, status.Name));
                    Bus.RaiseEvent(new TransactionRegisteredEvent(Guid.NewGuid(), message.Amount, message.IdTransactionType, message.IdCard, status.Id, message.Number, message.TransactionDate));
                }

                return Task.FromResult(false);
            }

            // cartao expirado
            if (card.ExpirationDate < DateTime.Now)
            {
                TransactionStatus statusNegada = GetItemStatus(config, "TransacaoNegada");
                var transactionNgada = new Transaction(message.Id, message.Amount, message.IdTransactionType, message.IdCard, statusNegada.Id, message.Number, DateTime.Now);
                _transactionTypeRepository.Add(transactionNgada);
                if (Commit())
                {
                    TransactionStatus status = GetItemStatus(config, "CartaoVencido");
                    Bus.RaiseEvent(new DomainNotification(message.MessageType, status.Name));
                    Bus.RaiseEvent(new TransactionRegisteredEvent(Guid.NewGuid(), message.Amount, message.IdTransactionType, message.IdCard, status.Id, message.Number, message.TransactionDate));
                }

                return Task.FromResult(false);
            }

            // Validate Password or blocked card - comparar a senha da transacao com a senha do cartao pega do banco de dados
            if (Convert.ToBoolean(message.HasPassword) && !message.Password.Equals(card.Password))
            {
                int countAttempt = card.Attempts + 1;

                card.Attempts = countAttempt;

                // bloquear cartao apos 3 tentativas incorretas de senha
                if (countAttempt >= 3)
                {
                    card.Blocked = 0;

                    // atualizar dados do cartao
                    _cardRepository.Update(card);
                    if (Commit())
                    {
                        TransactionStatus statusNegada = GetItemStatus(config, "TransacaoNegada");
                        var transactionNgada = new Transaction(message.Id, message.Amount, message.IdTransactionType, message.IdCard, statusNegada.Id, message.Number, DateTime.Now);
                        _transactionTypeRepository.Add(transactionNgada);
                        if (Commit())
                        {
                            TransactionStatus status = GetItemStatus(config, "CartaoBloqueado");
                            Bus.RaiseEvent(new DomainNotification(message.MessageType, status.Name));
                            Bus.RaiseEvent(new TransactionRegisteredEvent(Guid.NewGuid(), message.Amount, message.IdTransactionType, message.IdCard, status.Id, message.Number, message.TransactionDate));
                        }
                    }
                }
                else
                {
                    _cardRepository.Update(card);
                    if (Commit())
                    {
                        TransactionStatus statusNegada = GetItemStatus(config, "TransacaoNegada");
                        var transactionNgada = new Transaction(message.Id, message.Amount, message.IdTransactionType, message.IdCard, statusNegada.Id, message.Number, DateTime.Now);
                        _transactionTypeRepository.Add(transactionNgada);
                        if (Commit())
                        {
                            TransactionStatus status = GetItemStatus(config, "SenhaIncorreta");
                            Bus.RaiseEvent(new DomainNotification(message.MessageType, status.Name));
                            Bus.RaiseEvent(new TransactionRegisteredEvent(Guid.NewGuid(), message.Amount, message.IdTransactionType, message.IdCard, status.Id, message.Number, message.TransactionDate));
                        }
                    }
                }

                return Task.FromResult(false);
            }

            // cartao com saldo insuficiente para compra
            if (message.Amount > card.LimitAvailable)
            {
                TransactionStatus statusNegada = GetItemStatus(config, "TransacaoNegada");
                var transactionNgada = new Transaction(message.Id, message.Amount, message.IdTransactionType, message.IdCard, statusNegada.Id, message.Number, DateTime.Now);
                _transactionTypeRepository.Add(transactionNgada);
                if (Commit())
                {
                    TransactionStatus status = GetItemStatus(config, "SaldoInsuficiente");
                    Bus.RaiseEvent(new DomainNotification(message.MessageType, status.Name));
                    Bus.RaiseEvent(new TransactionRegisteredEvent(Guid.NewGuid(), message.Amount, message.IdTransactionType, message.IdCard, status.Id, message.Number, message.TransactionDate));
                }

                return Task.FromResult(false);
            }

            decimal changeCardLimitAvailable = card.LimitAvailable - message.Amount;
            card.LimitAvailable = changeCardLimitAvailable;

            TransactionStatus statusWouu = GetItemStatus(config, "TransacaoAprovada");

            var transaction = new Transaction(message.Id, message.Amount, message.IdTransactionType, message.IdCard, statusWouu.Id, message.Number, DateTime.Now);

            // wouuu - efetivar a transacao
            _transactionTypeRepository.Add(transaction);
            if (Commit())
            {
                _cardRepository.Update(card);
                if (Commit())
                    Bus.RaiseEvent(new TransactionRegisteredEvent(Guid.NewGuid(), message.Amount, message.IdTransactionType, message.IdCard, statusWouu.Id, message.Number, DateTime.Now));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateTransactionCommand message, CancellationToken cancellationToken)
        {
            return null;
        }

        public Task<bool> Handle(RemoveTransactionCommand message, CancellationToken cancellationToken)
        {
            //if (!message.IsValid())
            //{
            //    NotifyValidationErrors(message);
            //    return Task.FromResult(false);
            //}

            //Transaction transaction = _transactionTypeRepository.GetById(message.Id);
            //if (transaction == null)
            //{
            //    // notificar o dominio
            //    Bus.RaiseEvent(new DomainNotification(message.MessageType, "Registro não encontrado"));

            //    return Task.FromResult(false);
            //}

            //_transactionTypeRepository.Remove(message.Id);

            //if (Commit())
            //{
            //    Bus.RaiseEvent(new TransactionRemovedEvent(message.Id));
            //}

            //return Task.FromResult(true);

            return null;
        }

        public TransactionStatus GetItemStatus(IConfigurationRoot config, string configName)
        {
            // pegar informacoes do status
            TransactionStatus itemStatus = _transactionStatusRepository
            .GetByName(config.GetSection("TransactionStatus:" + configName + "").Value);

            return itemStatus;
        }

        public void Dispose()
        {
            _cardRepository.Dispose();
            _transactionTypeRepository.Dispose();
            _transactionStatusRepository.Dispose();
        }
    }
}