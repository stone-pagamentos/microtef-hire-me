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
using System.Collections.Generic;
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

            #region Dados Mockados

            List<TransactionStatusMock> listTransactionStatus = new List<TransactionStatusMock>();

            listTransactionStatus.Add(new TransactionStatusMock
            {
                Id = new Guid("9B8745FD-BDA2-41D3-8F49-8C893461FBE1"),
                Name = "Valor inválido",
                NameTag = "ValorInvalido"
            });

            listTransactionStatus.Add(new TransactionStatusMock
            {
                Id = new Guid("47748064-E751-4E2C-93DC-E4A8FF2388BC"),
                Name = "Transação negada",
                NameTag = "TransacaoNegada"
            });

            listTransactionStatus.Add(new TransactionStatusMock
            {
                Id = new Guid("105DCD64-4498-4B06-8A74-914D1020DBE4"),
                Name = "Transação aprovada",
                NameTag = "TransacaoAprovada"
            });

            listTransactionStatus.Add(new TransactionStatusMock
            {
                Id = new Guid("D5117C58-2962-4B2A-938E-05698AA76A2B"),
                Name = "Senha inválida",
                NameTag = "SenhaInvalida"
            });

            listTransactionStatus.Add(new TransactionStatusMock
            {
                Id = new Guid("FCD03359-22A7-41E3-B9DE-70F8FBA47805"),
                Name = "Senha Incorreta",
                NameTag = "SenhaIncorreta"
            });

            listTransactionStatus.Add(new TransactionStatusMock
            {
                Id = new Guid("7614057A-AB4B-4447-82D5-EF88A097CBD4"),
                Name = "Erro no tamanho da senha",
                NameTag = "ErroNoTamanhoDaSenha"

            });

            listTransactionStatus.Add(new TransactionStatusMock
            {
                Id = new Guid("D87358AF-6800-4545-BB11-A6114627DF6A"),
                Name = "Saldo insuficiente",
                NameTag = "SaldoInsuficiente"
            });

            listTransactionStatus.Add(new TransactionStatusMock
            {
                Id = new Guid("A92807BA-A7A8-4BFF-B198-502A26306E53"),
                Name = "Cartão bloqueado",
                NameTag = "CartaoBloqueado"
            });

            listTransactionStatus.Add(new TransactionStatusMock
            {
                Id = new Guid("61F61682-65B9-424A-BB1B-7B04B3264114"),
                Name = "Aprovado",
                NameTag = "Aprovado"
            });

            listTransactionStatus.Add(new TransactionStatusMock
            {
                Id = new Guid("A73D8B79-8985-4CF8-AC2C-5C4AA4B84BA5"),
                Name = "Registro não encontrado",
                NameTag = "RegistroNaoEncontrado"
            });

            listTransactionStatus.Add(new TransactionStatusMock
            {
                Id = new Guid("E23F6900-CB87-475A-9DBE-E490552967C3"),
                Name = "Senha entre 4 e 6 dítigos",
                NameTag = "SenhaEntre4e6Ditigos"
            });

            listTransactionStatus.Add(new TransactionStatusMock
            {
                Id = new Guid("526F6B9A-2248-49AE-8119-56F03C9A9824"),
                Name = "Mínimo de 10 centavos",
                NameTag = "MinimoDe10Centavos"
            });

            listTransactionStatus.Add(new TransactionStatusMock
            {
                Id = new Guid("7614057a-ab4b-4447-82d5-ef88a097cbd9"),
                Name = "Cartão vencido",
                NameTag = "CartaoVencido"
            });

            #endregion

            // pegar informacoes do cartao
            Card card = _cardRepository.GetById(message.IdCard);

            // cartao bloqueado
            if (card.Blocked == 0)
            {
                TransactionStatus statusNegada = GetItemStatus(listTransactionStatus.Find(x => x.NameTag.Equals("TransacaoNegada")).Name);
                var transactionNgada = new Transaction(message.Id, message.Amount, message.IdTransactionType, message.IdCard, statusNegada.Id, message.Number, DateTime.Now);
                _transactionTypeRepository.Add(transactionNgada);
                if (Commit())
                {
                    TransactionStatus status = GetItemStatus(listTransactionStatus.Find(x => x.NameTag.Equals("CartaoBloqueado")).Name);
                    Bus.RaiseEvent(new DomainNotification(message.MessageType, status.Name));
                    Bus.RaiseEvent(new TransactionRegisteredEvent(Guid.NewGuid(), message.Amount, message.IdTransactionType, message.IdCard, status.Id, message.Number, message.TransactionDate));
                }

                return Task.FromResult(false);
            }

            // cartao expirado
            if (card.ExpirationDate < DateTime.Now)
            {
                TransactionStatus statusNegada = GetItemStatus(listTransactionStatus.Find(x => x.NameTag.Equals("TransacaoNegada")).Name);
                var transactionNgada = new Transaction(message.Id, message.Amount, message.IdTransactionType, message.IdCard, statusNegada.Id, message.Number, DateTime.Now);
                _transactionTypeRepository.Add(transactionNgada);
                if (Commit())
                {
                    TransactionStatus status = GetItemStatus(listTransactionStatus.Find(x => x.NameTag.Equals("CartaoVencido")).Name);
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
                        TransactionStatus statusNegada = GetItemStatus(listTransactionStatus.Find(x => x.NameTag.Equals("TransacaoNegada")).Name);
                        var transactionNgada = new Transaction(message.Id, message.Amount, message.IdTransactionType, message.IdCard, statusNegada.Id, message.Number, DateTime.Now);
                        _transactionTypeRepository.Add(transactionNgada);
                        if (Commit())
                        {
                            TransactionStatus status = GetItemStatus(listTransactionStatus.Find(x => x.NameTag.Equals("CartaoBloqueado")).Name);
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
                        TransactionStatus statusNegada = GetItemStatus(listTransactionStatus.Find(x => x.NameTag.Equals("TransacaoNegada")).Name);
                        var transactionNgada = new Transaction(message.Id, message.Amount, message.IdTransactionType, message.IdCard, statusNegada.Id, message.Number, DateTime.Now);
                        _transactionTypeRepository.Add(transactionNgada);
                        if (Commit())
                        {
                            TransactionStatus status = GetItemStatus(listTransactionStatus.Find(x => x.NameTag.Equals("SenhaIncorreta")).Name);
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
                TransactionStatus statusNegada = GetItemStatus(listTransactionStatus.Find(x => x.NameTag.Equals("TransacaoNegada")).Name);
                var transactionNgada = new Transaction(message.Id, message.Amount, message.IdTransactionType, message.IdCard, statusNegada.Id, message.Number, DateTime.Now);
                _transactionTypeRepository.Add(transactionNgada);
                if (Commit())
                {
                    TransactionStatus status = GetItemStatus(listTransactionStatus.Find(x => x.NameTag.Equals("SaldoInsuficiente")).Name);
                    Bus.RaiseEvent(new DomainNotification(message.MessageType, status.Name));
                    Bus.RaiseEvent(new TransactionRegisteredEvent(Guid.NewGuid(), message.Amount, message.IdTransactionType, message.IdCard, status.Id, message.Number, message.TransactionDate));
                }

                return Task.FromResult(false);
            }

            decimal changeCardLimitAvailable = card.LimitAvailable - message.Amount;
            card.LimitAvailable = changeCardLimitAvailable;

            TransactionStatus statusWouu = GetItemStatus(listTransactionStatus.Find(x => x.NameTag.Equals("TransacaoAprovada")).Name);

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

        public TransactionStatus GetItemStatus(string configName)
        {
            // pegar informacoes do status
            TransactionStatus itemStatus = _transactionStatusRepository
            .GetByName(configName);

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