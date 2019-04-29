using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Karnak.Application.ViewModels
{
    public class CardBrandViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [DisplayName("Name")]
        public string Name { get; set; }
    }
}
