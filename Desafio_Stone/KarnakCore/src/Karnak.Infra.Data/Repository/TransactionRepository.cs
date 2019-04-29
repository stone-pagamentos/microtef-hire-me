using Karnak.Domain.Interfaces;
using Karnak.Domain.Models;
using Karnak.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

namespace Karnak.Infra.Data.Repository
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        private readonly IHostingEnvironment _env;

        public TransactionRepository(KarnakContext context, IHostingEnvironment env)
            : base(context)
        {
            _env = env;
        }

        public Transaction GetByName(string name)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Id.ToString() == name);
        }

        public List<TransactionList> SondagemTransacoes(string cardNumber)
        {
            using (var db = new KarnakContext(_env))
            {
                var trans = from transaction in db.Transaction

                            join transType in db.TransactionType
                                on transaction.IdTransactionType equals transType.Id

                            join transStatus in db.TransactionStatus
                                on transaction.IdTransactionStatus equals transStatus.Id

                            join card in db.Card
                                on transaction.IdCard equals card.Id

                            join cardBrand in db.CardBrand
                                on card.IdBrand equals cardBrand.Id

                            join cardType in db.CardType
                                on card.IdCardType equals cardType.Id

                            join customer in db.Customer
                                on card.IdCustomer equals customer.Id

                            where (card.CardNumber.Equals(cardNumber))

                            orderby transaction.TransactionDate descending, transaction.Amount descending

                            select new TransactionList
                            {
                                // Transaction
                                TransactionId = transaction.Id,
                                TransactionCardId = transaction.IdCard,
                                TransactionAmount = transaction.Amount,
                                TransactionNumber = transaction.Number,
                                TransactionDate = transaction.TransactionDate,

                                // Transaction Type
                                TransactionTypeId = transaction.IdTransactionType,
                                TransactionTypeName = transType.Name,

                                // Transaction Status
                                TransactionStatusId = transaction.IdTransactionStatus,
                                TransactionStatusName = transStatus.Name,

                                // Card Brand
                                CardBrandId = card.IdBrand,
                                CardBrandName = cardBrand.Name,

                                // Card Type
                                CardTypeId = card.IdCardType,
                                CardTypeName = cardType.Name,

                                // Customer
                                CustomerId = card.IdCustomer,
                                CustomerName = customer.Name,
                                CustomerBirthDate = customer.BirthDate,
                                CustomerEmail = customer.Email,

                                // Card
                                CardId = card.Id,
                                CardCardNumber = card.CardNumber,
                                CardExpirationDate = card.ExpirationDate,
                                CardHasPassword = card.HasPassword,
                                CardPassword = card.Password,
                                CardLimit = card.Limit,
                                CardLimitAvailable = card.LimitAvailable,
                                CardAttempts = card.Attempts,
                                CardBlocked = card.Blocked
                            };

                return trans.ToList();
            }
        }

        public List<TransactionList> TransactionList()
        {
            using (var db = new KarnakContext(_env))
            {
                var trans = from transaction in db.Transaction

                            join transType in db.TransactionType
                                on transaction.IdTransactionType equals transType.Id

                            join transStatus in db.TransactionStatus
                                on transaction.IdTransactionStatus equals transStatus.Id

                            join card in db.Card
                                on transaction.IdCard equals card.Id

                            join cardBrand in db.CardBrand
                                on card.IdBrand equals cardBrand.Id

                            join cardType in db.CardType
                                on card.IdCardType equals cardType.Id

                            join customer in db.Customer
                                on card.IdCustomer equals customer.Id

                            orderby transaction.TransactionDate descending, card.CardNumber

                            select new TransactionList
                            {
                                // Transaction
                                TransactionId = transaction.Id,
                                TransactionCardId = transaction.IdCard,
                                TransactionAmount = transaction.Amount,
                                TransactionNumber = transaction.Number,
                                TransactionDate = transaction.TransactionDate,

                                // Transaction Type
                                TransactionTypeId = transaction.IdTransactionType,
                                TransactionTypeName = transType.Name,
                                
                                // Transaction Status
                                TransactionStatusId = transaction.IdTransactionStatus,
                                TransactionStatusName = transStatus.Name,

                                // Card Brand
                                CardBrandId = card.IdBrand,
                                CardBrandName = cardBrand.Name,

                                // Card Type
                                CardTypeId = card.IdCardType,
                                CardTypeName = cardType.Name,

                                // Customer
                                CustomerId = card.IdCustomer,
                                CustomerName = customer.Name,
                                CustomerBirthDate = customer.BirthDate,
                                CustomerEmail = customer.Email,

                                // Card
                                CardId = card.Id,
                                CardCardNumber = card.CardNumber,
                                CardExpirationDate = card.ExpirationDate,
                                CardHasPassword = card.HasPassword,
                                CardPassword = card.Password,
                                CardLimit = card.Limit,
                                CardLimitAvailable = card.LimitAvailable,
                                CardAttempts = card.Attempts,
                                CardBlocked = card.Blocked
                            };

                return trans.ToList();
            }
        }
    }
}
