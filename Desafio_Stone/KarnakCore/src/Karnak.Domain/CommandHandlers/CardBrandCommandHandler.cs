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
    public class CardBrandCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewCardBrandCommand, bool>,
        IRequestHandler<UpdateCardBrandCommand, bool>,
        IRequestHandler<RemoveCardBrandCommand, bool>
    {
        private readonly ICardBrandRepository _cardBrandRepository;
        private readonly IMediatorHandler Bus;

        public CardBrandCommandHandler(ICardBrandRepository cardBrandRepository, 
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<DomainNotification> notifications) :base(uow, bus, notifications)
        {
            _cardBrandRepository = cardBrandRepository;
            Bus = bus;
        }

        public Task<bool> Handle(RegisterNewCardBrandCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            //var cardBrand = new CardBrand(Guid.NewGuid(), message.Name);
            var cardBrand = new CardBrand(message.Id, message.Name);

            if (_cardBrandRepository.GetByName(message.Name) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The card brand name has already been taken."));
                return Task.FromResult(false);
            }

            if (_cardBrandRepository.GetById(message.Id) != null)
            {
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "The card brand id has already been taken."));
                return Task.FromResult(false);
            }

            _cardBrandRepository.Add(cardBrand);

            if (Commit())
            {
                Bus.RaiseEvent(new CardBrandRegisteredEvent(cardBrand.Id, cardBrand.Name));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(UpdateCardBrandCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            var cardBrand = new CardBrand(message.Id, message.Name);
            var existingCardBrand = _cardBrandRepository.GetByName(cardBrand.Name);

            if (existingCardBrand != null && existingCardBrand.Id != cardBrand.Id)
            {
                if (!existingCardBrand.Equals(cardBrand))
                {
                    Bus.RaiseEvent(new DomainNotification(message.MessageType,"The card brand name has already been taken."));
                    return Task.FromResult(false);
                }
            }

            _cardBrandRepository.Update(cardBrand);

            if (Commit())
            {
                Bus.RaiseEvent(new CardBrandUpdatedEvent(cardBrand.Id, cardBrand.Name));
            }

            return Task.FromResult(true);
        }

        public Task<bool> Handle(RemoveCardBrandCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                NotifyValidationErrors(message);
                return Task.FromResult(false);
            }

            CardBrand cardBrand = _cardBrandRepository.GetById(message.Id);
            if (cardBrand == null)
            {
                // notificar o dominio
                Bus.RaiseEvent(new DomainNotification(message.MessageType, "Registro não encontrado"));

                return Task.FromResult(false);
            }

            _cardBrandRepository.Remove(message.Id);

            if (Commit())
            {
                Bus.RaiseEvent(new CardBrandRemovedEvent(message.Id));
            }

            return Task.FromResult(true);
        }

        public void Dispose()
        {
            _cardBrandRepository.Dispose();
        }
    }
}