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
    public class CardTypeCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewCardTypeCommand, bool>,
        IRequestHandler<UpdateCardTypeCommand, bool>,
        IRequestHandler<RemoveCardTypeCommand, bool>
    {
        private readonly ICardTypeRepository _cardTypeRepository;
        private readonly IMediatorHandler Bus;

        public CardTypeCommandHandler(ICardTypeRepository cardTypeRepository, 
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) :base(uow, bus, notifications)
        {
            _cardTypeRepository = cardTypeRepository;
            Bus = bus;
        }

        public Task<bool> Handle(RegisterNewCardTypeCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var transactionType = new CardType(message.Id, message.Name);

            if (_cardTypeRepository.GetByName(message.Name) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The card type name has already been taken."));
                return Task.FromResult(false);
            }

            if (_cardTypeRepository.GetById(message.Id) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The card type id has already been taken."));
                return Task.FromResult(false);
            }

            _cardTypeRepository.Add(transactionType);

            if (Commit())
            {
                Bus.RaiseEvent(new CardTypeRegisteredEvent(transactionType.Id, transactionType.Name));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateCardTypeCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var transactionType = new CardType(message.Id, message.Name);
            var existingCardType = _cardTypeRepository.GetByName(transactionType.Name);

            if (existingCardType != null && existingCardType.Id != transactionType.Id)
            {
                if (!existingCardType.Equals(transactionType))
                {
                    Bus.RaiseEvent(new DomainNotification(message.MessageType, "The card type name has already been taken."));
                    return Task.FromResult(false);
                }
            }

            _cardTypeRepository.Update(transactionType);

            if (Commit())
            {
                Bus.RaiseEvent(new CardTypeUpdatedEvent(message.Id, message.Name));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveCardTypeCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            CardType cardType = _cardTypeRepository.GetById(message.Id);
            if(cardType == null)
            {
                // notificar o dominio
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "Registro não encontrado"));

                return Task.FromResult(false);
            }

            _cardTypeRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new CardTypeRemovedEvent(message.Id));
            }

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _cardTypeRepository.Dispose();
        }
    }
}