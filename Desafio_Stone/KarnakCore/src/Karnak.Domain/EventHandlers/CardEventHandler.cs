using System.Threading;
using System.Threading.Tasks;
using Karnak.Domain.Events;
using MediatR;

namespace Karnak.Domain.EventHandlers
{
    public class CardEventHandler :
        INotificationHandler<CardRegisteredEvent>,
        INotificationHandler<CardUpdatedEvent>,
        INotificationHandler<CardRemovedEvent>
    {
        public Task Handle(CardUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(CardRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(CardRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}