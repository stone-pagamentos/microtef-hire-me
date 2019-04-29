using System.Threading;
using System.Threading.Tasks;
using Karnak.Domain.Events;
using MediatR;

namespace Karnak.Domain.EventHandlers
{
    public class TransactionStatusEventHandler :
        INotificationHandler<TransactionStatusRegisteredEvent>,
        INotificationHandler<TransactionStatusUpdatedEvent>,
        INotificationHandler<TransactionStatusRemovedEvent>
    {
        public Task Handle(TransactionStatusUpdatedEvent message, CancellationToken cancellationToken)
        {
            // Send some notification e-mail

            return Task.CompletedTask;
        }

        public Task Handle(TransactionStatusRegisteredEvent message, CancellationToken cancellationToken)
        {
            // Send some greetings e-mail

            return Task.CompletedTask;
        }

        public Task Handle(TransactionStatusRemovedEvent message, CancellationToken cancellationToken)
        {
            // Send some see you soon e-mail

            return Task.CompletedTask;
        }
    }
}