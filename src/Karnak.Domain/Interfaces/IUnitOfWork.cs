using System;

namespace Karnak.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
