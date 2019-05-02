using Karnak.Domain.Commands;
using Karnak.Domain.Common;
using Karnak.Domain.Core.Bus;
using Karnak.Domain.Core.Notifications;
using Karnak.Domain.Events;
using Karnak.Domain.Interfaces;
using Karnak.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Karnak.Domain.CommandHandlers
{
    public class CardCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewCardCommand, bool>,
        IRequestHandler<UpdateCardCommand, bool>,
        IRequestHandler<RemoveCardCommand, bool>
    {
        private readonly ICardRepository _cardRepository;
        private readonly IMediatorHandler Bus;

        public CardCommandHandler(ICardRepository cardRepository, 
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) :base(uow, bus, notifications)
        {
            _cardRepository = cardRepository;
            Bus = bus;
        }

        public Task<bool> Handle(RegisterNewCardCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var card = new Card(message.Id, message.IdCustomer, message.IdBrand, message.IdCardType, message.CardNumber, message.ExpirationDate, message.HasPassword, message.Password, message.Limit, message.LimitAvailable, message.Attempts, message.Blocked);

            if (_cardRepository.GetByCardNumber(message.CardNumber) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The card number has already been taken."));
                return Task.FromResult(false);
            }

            if (_cardRepository.GetById(message.Id) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The card id has already been taken."));
                return Task.FromResult(false);
            }

            _cardRepository.Add(card);

            if (Commit())
            {
                Bus.RaiseEvent(new CardRegisteredEvent(card.Id, message.IdCustomer, message.IdBrand, message.IdCardType, message.CardNumber, message.ExpirationDate, message.HasPassword, StringCipher.Encrypt(message.Password, "StefanSilva@#@Stone##2019"), message.Limit, message.LimitAvailable, message.Attempts, message.Blocked));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateCardCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var card = new Card(message.Id, message.IdCustomer, message.IdBrand, message.IdCardType, message.CardNumber, message.ExpirationDate, message.HasPassword, message.Password, message.Limit, message.LimitAvailable, message.Attempts, message.Blocked);
            var existingCard = _cardRepository.GetByCardNumber(card.CardNumber);

            if (existingCard != null && existingCard.Id != card.Id)
            {
                if (!existingCard.Equals(card))
                {
                    Bus.RaiseEvent(new DomainNotification(message.MessageType, "The card number has already been taken."));
                    return Task.FromResult(false);
                }
            }

            _cardRepository.Update(card);

            if (Commit())
            {
                Bus.RaiseEvent(new CardUpdatedEvent(message.Id, message.IdCustomer, message.IdBrand, message.IdCardType, message.CardNumber, message.ExpirationDate, message.HasPassword, StringCipher.Encrypt(message.Password, "StefanSilva@#@Stone##2019"), message.Limit, message.LimitAvailable, message.Attempts, message.Blocked));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveCardCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            Card card = _cardRepository.GetById(message.Id);
            if (card == null)
            {
                // notificar o dominio
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "Registro não encontrado"));

                return Task.FromResult(false);
            }

            _cardRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new CardRemovedEvent(message.Id));
            }

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _cardRepository.Dispose();
        }
    }
}