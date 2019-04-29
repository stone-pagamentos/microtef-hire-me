using System.Linq;
using Karnak.Domain.Interfaces;
using Karnak.Domain.Models;
using Karnak.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Karnak.Infra.Data.Repository
{
    public class TransactionTypeRepository : Repository<TransactionType>, ITransactionTypeRepository
    {
        public TransactionTypeRepository(KarnakContext context)
            : base(context)
        {

        }

        public TransactionType GetByName(string name)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Name == name);
        }
    }
}
