using Karnak.Domain.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Karnak.Application.ViewModels
{
    public class TransactionViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Amount is Required")]
        [DisplayName("Amount")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "The IdTransactionType is Required")]
        [DisplayName("IdTransactionType")]
        public Guid IdTransactionType { get; set; }

        [Required(ErrorMessage = "The IdCard is Required")]
        [DisplayName("IdCard")]
        public Guid IdCard { get; set; }

        [Required(ErrorMessage = "The IdTransactionStatus is Required")]
        [DisplayName("IdTransactionStatus")]
        public Guid IdTransactionStatus { get; set; }

        [Required(ErrorMessage = "The Number is Required")]
        [DisplayName("Number")]
        public int Number { get; set; }

        [Required(ErrorMessage = "The TransactionDate is Required")]
        [DisplayName("TransactionDate")]
        public DateTime TransactionDate { get; set; }

        // a senha não é armazenada na tabela de transação
        public string Password { get; set; }

        public string HasPassword { get; set; }
    }
}
