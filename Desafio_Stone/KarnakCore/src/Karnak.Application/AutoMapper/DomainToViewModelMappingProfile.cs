using AutoMapper;
using Karnak.Application.ViewModels;
using Karnak.Domain.Models;

namespace Karnak.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<TransactionType, CustomerViewModel>();
            CreateMap<TransactionStatus, CustomerViewModel>();
            CreateMap<Transaction, CustomerViewModel>();
            CreateMap<CardBrand, CustomerViewModel>();
            CreateMap<Card, CustomerViewModel>();
            CreateMap<CardType, CustomerViewModel>();
            CreateMap<Customer, CustomerViewModel>();
        }
    }
}
