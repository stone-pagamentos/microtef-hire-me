using System.Linq;
using Karnak.Domain.Interfaces;
using Karnak.Domain.Models;
using Karnak.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Karnak.Infra.Data.Repository
{
    public class CardBrandRepository : Repository<CardBrand>, ICardBrandRepository
    {
        public CardBrandRepository(KarnakContext context)
            : base(context)
        {

        }

        public CardBrand GetByName(string name)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Name == name);
        }
    }
}
