using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Karnak.Application.ViewModels
{
    public class CardViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The IdCardType is Required")]
        [DisplayName("IdCardType")]
        public Guid IdCardType { get; set; }

        [Required(ErrorMessage = "The IdCustomer is Required")]
        [DisplayName("IdCustomer")]
        public Guid IdCustomer { get; set; }

        [Required(ErrorMessage = "The IdBrand is Required")]
        [DisplayName("IdBrand")]
        public Guid IdBrand { get; set; }

        [Required(ErrorMessage = "The CardNumber is Required")]
        [DisplayName("CardNumber")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "The ExpirationDate is Required")]
        [DisplayName("ExpirationDate")]
        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "The HasPassword is Required")]
        [DisplayName("HasPassword")]
        public int HasPassword { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The Limit is Required")]
        [DisplayName("Limit")]
        public decimal Limit { get; set; }

        [Required(ErrorMessage = "The LimitAvailable is Required")]
        [DisplayName("LimitAvailable")]
        public decimal LimitAvailable { get; set; }

        [Required(ErrorMessage = "The Attempts is Required")]
        [DisplayName("Attempts")]
        public int Attempts { get; set; }

        [Required(ErrorMessage = "The Blocked is Required")]
        [DisplayName("Blocked")]
        public int Blocked { get; set; }
    }
}
