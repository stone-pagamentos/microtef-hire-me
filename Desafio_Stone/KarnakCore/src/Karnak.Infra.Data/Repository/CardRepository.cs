using Karnak.Domain.Interfaces;
using Karnak.Domain.Models;
using Karnak.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Karnak.Infra.Data.Repository
{
    public class CardRepository : Repository<Card>, ICardRepository
    {
        public CardRepository(KarnakContext context)
            : base(context)
        {

        }

        public Card GetByCardNumber(string cardNumber)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.CardNumber == cardNumber);
        }
    }
}
