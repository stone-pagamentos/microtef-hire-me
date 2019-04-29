using System.Linq;
using Karnak.Domain.Interfaces;
using Karnak.Domain.Models;
using Karnak.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Karnak.Infra.Data.Repository
{
    public class CardTypeRepository : Repository<CardType>, ICardTypeRepository
    {
        public CardTypeRepository(KarnakContext context)
            : base(context)
        {

        }

        public CardType GetByName(string name)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Name == name);
        }
    }
}
