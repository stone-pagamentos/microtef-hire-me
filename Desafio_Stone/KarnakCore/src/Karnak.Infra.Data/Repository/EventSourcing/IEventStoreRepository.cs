using System;
using System.Collections.Generic;
using Karnak.Domain.Core.Events;

namespace Karnak.Infra.Data.Repository.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        IList<StoredEvent> All(Guid aggregateId);
    }
}