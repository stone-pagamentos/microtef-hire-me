using System.Threading.Tasks;
using Karnak.Domain.Core.Commands;
using Karnak.Domain.Core.Events;


namespace Karnak.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
