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
    public class TransactionTypeCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewTransactionTypeCommand, bool>,
        IRequestHandler<UpdateTransactionTypeCommand, bool>,
        IRequestHandler<RemoveTransactionTypeCommand, bool>
    {
        private readonly ITransactionTypeRepository _transactionTypeRepository;
        private readonly IMediatorHandler Bus;

        public TransactionTypeCommandHandler(ITransactionTypeRepository transactionTypeRepository, 
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) :base(uow, bus, notifications)
        {
            _transactionTypeRepository = transactionTypeRepository;
            Bus = bus;
        }

        public Task<bool> Handle(RegisterNewTransactionTypeCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var transactionType = new TransactionType(message.Id, message.Name);

            if (_transactionTypeRepository.GetByName(message.Name) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The transaction type name has already been taken."));
                return Task.FromResult(false);
            }

            if (_transactionTypeRepository.GetById(message.Id) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The transaction type id has already been taken."));
                return Task.FromResult(false);
            }

            _transactionTypeRepository.Add(transactionType);

            if (Commit())
            {
                Bus.RaiseEvent(new TransactionTypeRegisteredEvent(transactionType.Id, transactionType.Name));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateTransactionTypeCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var transactionType = new TransactionType(message.Id, message.Name);
            var existingTransactionType = _transactionTypeRepository.GetByName(transactionType.Name);

            if (existingTransactionType != null && existingTransactionType.Id != transactionType.Id)
            {
                if (!existingTransactionType.Equals(transactionType))
                {
                    Bus.RaiseEvent(new DomainNotification(message.MessageType,"The transaction type name has already been taken."));
                    return Task.FromResult(false);
                }
            }

            _transactionTypeRepository.Update(transactionType);

            if (Commit())
            {
                Bus.RaiseEvent(new TransactionTypeUpdatedEvent(transactionType.Id, transactionType.Name));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveTransactionTypeCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            TransactionType transactionType = _transactionTypeRepository.GetById(message.Id);
            if (transactionType == null)
            {
                // notificar o dominio
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "Registro não encontrado"));

                return Task.FromResult(false);
            }

            _transactionTypeRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new TransactionTypeRemovedEvent(message.Id));
            }

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _transactionTypeRepository.Dispose();
        }
    }
}