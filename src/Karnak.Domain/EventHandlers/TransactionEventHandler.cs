using System.Threading;
using System.Threading.Tasks;
using Karnak.Domain.Events;
using MediatR;

namespace Karnak.Domain.EventHandlers
{
    public class TransactionEventHandler :
        INotificationHandler<TransactionRegisteredEvent>,
        INotificationHandler<TransactionUpdatedEvent>,
        INotificationHandler<TransactionRemovedEvent>
    {
        public Task Handle(TransactionUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(TransactionRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(TransactionRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}