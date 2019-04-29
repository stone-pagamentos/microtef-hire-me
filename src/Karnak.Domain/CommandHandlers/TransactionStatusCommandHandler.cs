using System;
using System.Threading;
using System.Threading.Tasks;
using Karnak.Domain.Commands;
using Karnak.Domain.Core.Bus;
using Karnak.Domain.Core.Notifications;
using Karnak.Domain.Events;
using Karnak.Domain.Interfaces;
using Karnak.Domain.Models;
using MediatR;

namespace Karnak.Domain.CommandHandlers
{
    public class TransactionStatusCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewTransactionStatusCommand, bool>,
        IRequestHandler<UpdateTransactionStatusCommand, bool>,
        IRequestHandler<RemoveTransactionStatusCommand, bool>
    {
        private readonly ITransactionStatusRepository _transactionStatusRepository;
        private readonly IMediatorHandler Bus;

        public TransactionStatusCommandHandler(ITransactionStatusRepository transactionStatusRepository, 
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) :base(uow, bus, notifications)
        {
            _transactionStatusRepository = transactionStatusRepository;
            Bus = bus;
        }

        public Task<bool> Handle(RegisterNewTransactionStatusCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var transactionType = new TransactionStatus(message.Id, message.Name);

            if (_transactionStatusRepository.GetByName(message.Name) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The transactionType name has already been taken."));
                return Task.FromResult(false);
            }
            
            _transactionStatusRepository.Add(transactionType);

            if (Commit())
            {
                Bus.RaiseEvent(new TransactionStatusRegisteredEvent(transactionType.Id, transactionType.Name));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateTransactionStatusCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var transactionType = new TransactionStatus(message.Id, message.Name);
            var existingTransactionStatus = _transactionStatusRepository.GetByName(transactionType.Name);

            if (existingTransactionStatus != null && existingTransactionStatus.Id != transactionType.Id)
            {
                if (!existingTransactionStatus.Equals(transactionType))
                {
                    Bus.RaiseEvent(new DomainNotification(message.MessageType,"The transaction status name has already been taken."));
                    return Task.FromResult(false);
                }
            }

            _transactionStatusRepository.Update(transactionType);

            if (Commit())
            {
                Bus.RaiseEvent(new TransactionStatusUpdatedEvent(transactionType.Id, transactionType.Name));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveTransactionStatusCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            TransactionStatus transactionStatus = _transactionStatusRepository.GetById(message.Id);
            if (transactionStatus == null)
            {
                // notificar o dominio
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "Registro não encontrado"));

                return Task.FromResult(false);
            }

            _transactionStatusRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new TransactionStatusRemovedEvent(message.Id));
            }

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _transactionStatusRepository.Dispose();
        }
    }
}