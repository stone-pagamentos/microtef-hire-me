using System.Linq;
using Karnak.Domain.Interfaces;
using Karnak.Domain.Models;
using Karnak.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Karnak.Infra.Data.Repository
{
    public class TransactionStatusRepository : Repository<TransactionStatus>, ITransactionStatusRepository
    {
        public TransactionStatusRepository(KarnakContext context)
            : base(context)
        {

        }

        public TransactionStatus GetByName(string name)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Name == name);
        }
    }
}
