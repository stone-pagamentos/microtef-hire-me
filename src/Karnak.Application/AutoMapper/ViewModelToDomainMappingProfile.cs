using AutoMapper;
using Karnak.Application.ViewModels;
using Karnak.Domain.Commands;

namespace Karnak.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            // Transaction Type Map
            CreateMap<TransactionTypeViewModel, RegisterNewTransactionTypeCommand>()
                .ConstructUsing(c => new RegisterNewTransactionTypeCommand(c.Name));
            CreateMap<TransactionTypeViewModel, UpdateTransactionTypeCommand>()
                .ConstructUsing(c => new UpdateTransactionTypeCommand(c.Id, c.Name));

            // Transaction Status
            CreateMap<TransactionStatusViewModel, RegisterNewTransactionStatusCommand>()
                .ConstructUsing(c => new RegisterNewTransactionStatusCommand(c.Name));
            CreateMap<TransactionStatusViewModel, UpdateTransactionStatusCommand>()
                .ConstructUsing(c => new UpdateTransactionStatusCommand(c.Id, c.Name));

            // Transaction
            CreateMap<TransactionViewModel, RegisterNewTransactionCommand>()
                .ConstructUsing(c => new RegisterNewTransactionCommand(c.Amount, c.IdTransactionType, c.IdCard, c.IdTransactionStatus, c.Number, c.TransactionDate, c.Password, c.HasPassword));
            CreateMap<TransactionViewModel, UpdateTransactionCommand>()
                .ConstructUsing(c => new UpdateTransactionCommand(c.Id, c.Amount, c.IdTransactionType, c.IdCard, c.IdTransactionStatus, c.Number, c.TransactionDate, c.Password));

            // Card Type Map
            CreateMap<CardTypeViewModel, RegisterNewCardTypeCommand>()
                .ConstructUsing(c => new RegisterNewCardTypeCommand(c.Name));
            CreateMap<CardTypeViewModel, UpdateCardTypeCommand>()
                .ConstructUsing(c => new UpdateCardTypeCommand(c.Id, c.Name));

            // Card Brand Map
            CreateMap<CardBrandViewModel, RegisterNewCardBrandCommand>()
                .ConstructUsing(c => new RegisterNewCardBrandCommand(c.Name));
            CreateMap<CardBrandViewModel, UpdateCardBrandCommand>()
                .ConstructUsing(c => new UpdateCardBrandCommand(c.Id, c.Name));

            // Customer Map
            CreateMap<CustomerViewModel, RegisterNewCustomerCommand>()
                .ConstructUsing(c => new RegisterNewCustomerCommand(c.Name, c.Email, c.BirthDate));
            CreateMap<CustomerViewModel, UpdateCustomerCommand>()
                .ConstructUsing(c => new UpdateCustomerCommand(c.Id, c.Name, c.Email, c.BirthDate));

            // Card Map
            CreateMap<CardViewModel, RegisterNewCardCommand>()
                .ConstructUsing(c => new RegisterNewCardCommand(c.IdCustomer, c.IdBrand, c.IdCardType, c.CardNumber, c.ExpirationDate, c.HasPassword, c.Password, c.Limit, c.LimitAvailable, c.Attempts, c.Blocked));
            CreateMap<CardViewModel, UpdateCardCommand>()
                .ConstructUsing(c => new UpdateCardCommand(c.Id, c.IdCustomer, c.IdBrand, c.IdCardType, c.CardNumber, c.ExpirationDate, c.HasPassword, c.Password, c.Limit, c.LimitAvailable, c.Attempts, c.Blocked));

        }
    }
}
