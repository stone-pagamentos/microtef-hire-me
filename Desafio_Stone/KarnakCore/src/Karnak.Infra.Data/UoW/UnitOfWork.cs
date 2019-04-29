using Karnak.Domain.Interfaces;
using Karnak.Infra.Data.Context;

namespace Karnak.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KarnakContext _context;

        public UnitOfWork(KarnakContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
