using System;

namespace Karnak.Domain.Models
{
    public class TransactionList
    {
        #region Card

        public Guid CardId { get; set; }

        public string CardCardNumber { get; set; }

        public DateTime CardExpirationDate { get; set; }

        public int CardHasPassword { get; set; }

        public string CardPassword { get; set; }

        public decimal CardLimit { get; set; }

        public decimal CardLimitAvailable { get; set; }

        public int CardAttempts { get; set; }

        public int CardBlocked { get; set; }

        #endregion



        #region Card Brand

        public Guid CardBrandId { get; set; }

        public string CardBrandName { get; set; }

        #endregion



        #region Card Type

        public Guid CardTypeId { get; set; }

        public string CardTypeName { get; set; }

        #endregion



        #region Transaction

        public Guid TransactionId { get; set; }

        public decimal TransactionAmount { get; set; }

        public Guid TransactionTypeId { get; set; }

        public Guid TransactionCardId { get; set; }

        public Guid TransactionStatusId { get; set; }

        public int TransactionNumber { get; set; }

        public DateTime TransactionDate { get; set; }

        #endregion



        #region Transaction Type

        public string TransactionTypeName { get; set; }

        #endregion



        #region Transaction Status

        public String TransactionStatusName { get; set; }

        #endregion



        #region Customer

        public Guid CustomerId { get; set; }

        public string CustomerName { get; set; }

        public DateTime CustomerBirthDate { get; set; }

        public string CustomerEmail { get; set; }

        #endregion

    }
}
