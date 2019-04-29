using System.Threading;
using System.Threading.Tasks;
using Karnak.Domain.Events;
using MediatR;

namespace Karnak.Domain.EventHandlers
{
    public class TransactionTypeEventHandler :
        INotificationHandler<TransactionTypeRegisteredEvent>,
        INotificationHandler<TransactionTypeUpdatedEvent>,
        INotificationHandler<TransactionTypeRemovedEvent>
    {
        public Task Handle(TransactionTypeUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(TransactionTypeRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(TransactionTypeRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}