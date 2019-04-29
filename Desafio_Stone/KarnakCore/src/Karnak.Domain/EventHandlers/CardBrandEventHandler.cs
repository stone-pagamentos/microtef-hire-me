using System.Threading;
using System.Threading.Tasks;
using Karnak.Domain.Events;
using MediatR;

namespace Karnak.Domain.EventHandlers
{
    public class CardBrandEventHandler :
        INotificationHandler<CardBrandRegisteredEvent>,
        INotificationHandler<CardBrandUpdatedEvent>,
        INotificationHandler<CardBrandRemovedEvent>
    {
        public Task Handle(CardBrandUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(CardBrandRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(CardBrandRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}