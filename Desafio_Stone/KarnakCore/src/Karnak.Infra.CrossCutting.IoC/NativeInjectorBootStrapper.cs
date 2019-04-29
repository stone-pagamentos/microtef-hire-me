using Karnak.Application.Interfaces;
using Karnak.Application.Services;
using Karnak.Domain.CommandHandlers;
using Karnak.Domain.Commands;
using Karnak.Domain.Core.Bus;
using Karnak.Domain.Core.Events;
using Karnak.Domain.Core.Notifications;
using Karnak.Domain.EventHandlers;
using Karnak.Domain.Events;
using Karnak.Domain.Interfaces;
using Karnak.Infra.CrossCutting.Bus;
using Karnak.Infra.CrossCutting.Identity.Authorization;
using Karnak.Infra.CrossCutting.Identity.Models;
using Karnak.Infra.CrossCutting.Identity.Services;
using Karnak.Infra.Data.Context;
using Karnak.Infra.Data.EventSourcing;
using Karnak.Infra.Data.Repository;
using Karnak.Infra.Data.Repository.EventSourcing;
using Karnak.Infra.Data.UoW;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Karnak.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // ASP.NET Authorization Polices
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();





            #region - Transaction Type

            // Application Service - Transaction Type
            services.AddScoped<ITransactionTypeAppService, TransactionTypeAppService>();

            // Domain - Events - Transaction Type
            services.AddScoped<INotificationHandler<TransactionTypeRegisteredEvent>, TransactionTypeEventHandler>();
            services.AddScoped<INotificationHandler<TransactionTypeUpdatedEvent>, TransactionTypeEventHandler>();
            services.AddScoped<INotificationHandler<TransactionTypeRemovedEvent>, TransactionTypeEventHandler>();

            // Domain - Commands - Transaction Type
            services.AddScoped<IRequestHandler<RegisterNewTransactionTypeCommand, bool>, TransactionTypeCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateTransactionTypeCommand, bool>, TransactionTypeCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveTransactionTypeCommand, bool>, TransactionTypeCommandHandler>();

            // Infra - Repository - Transaction Type
            services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();

            #endregion



            
            #region - Transaction Status

            // Application Service - Transaction Status
            services.AddScoped<ITransactionStatusAppService, TransactionStatusAppService>();

            // Domain - Events - Transaction Status
            services.AddScoped<INotificationHandler<TransactionStatusRegisteredEvent>, TransactionStatusEventHandler>();
            services.AddScoped<INotificationHandler<TransactionStatusUpdatedEvent>, TransactionStatusEventHandler>();
            services.AddScoped<INotificationHandler<TransactionStatusRemovedEvent>, TransactionStatusEventHandler>();

            // Domain - Commands - Transaction Status
            services.AddScoped<IRequestHandler<RegisterNewTransactionStatusCommand, bool>, TransactionStatusCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateTransactionStatusCommand, bool>, TransactionStatusCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveTransactionStatusCommand, bool>, TransactionStatusCommandHandler>();

            // Infra - Repository - Transaction Status
            services.AddScoped<ITransactionStatusRepository, TransactionStatusRepository>();

            #endregion



            
            #region - Transaction

            // Application Service - Transaction 
            services.AddScoped<ITransactionAppService, TransactionAppService>();

            // Domain - Events - Transaction 
            services.AddScoped<INotificationHandler<TransactionRegisteredEvent>, TransactionEventHandler>();
            services.AddScoped<INotificationHandler<TransactionUpdatedEvent>, TransactionEventHandler>();
            services.AddScoped<INotificationHandler<TransactionRemovedEvent>, TransactionEventHandler>();

            // Domain - Commands - Transaction 
            services.AddScoped<IRequestHandler<RegisterNewTransactionCommand, bool>, TransactionCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateTransactionCommand, bool>, TransactionCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveTransactionCommand, bool>, TransactionCommandHandler>();

            // Infra - Repository - Transaction 
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            #endregion



            
            #region  - Card Brand

            // Application Service - Card Brand
            services.AddScoped<ICardBrandAppService, CardBrandAppService>();

            // Domain - Events - Card Brand
            services.AddScoped<INotificationHandler<CardBrandRegisteredEvent>, CardBrandEventHandler>();
            services.AddScoped<INotificationHandler<CardBrandUpdatedEvent>, CardBrandEventHandler>();
            services.AddScoped<INotificationHandler<CardBrandRemovedEvent>, CardBrandEventHandler>();

            // Domain - Commands - Card Brand
            services.AddScoped<IRequestHandler<RegisterNewCardBrandCommand, bool>, CardBrandCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCardBrandCommand, bool>, CardBrandCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCardBrandCommand, bool>, CardBrandCommandHandler>();

            // Infra - Repository - Card Brand
            services.AddScoped<ICardBrandRepository, CardBrandRepository>();

            #endregion



            
            #region - Card 

            // Application Service - Card 
            services.AddScoped<ICardAppService, CardAppService>();

            // Domain - Events - Card 
            services.AddScoped<INotificationHandler<CardRegisteredEvent>, CardEventHandler>();
            services.AddScoped<INotificationHandler<CardUpdatedEvent>, CardEventHandler>();
            services.AddScoped<INotificationHandler<CardRemovedEvent>, CardEventHandler>();

            // Domain - Commands - Card 
            services.AddScoped<IRequestHandler<RegisterNewCardCommand, bool>, CardCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCardCommand, bool>, CardCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCardCommand, bool>, CardCommandHandler>();

            // Infra - Repository - Card 
            services.AddScoped<ICardRepository, CardRepository>();

            #endregion




            #region - Card Type

            // Application Service - Card Type
            services.AddScoped<ICardTypeAppService, CardTypeAppService>();

            // Domain - Events - Card Type
            services.AddScoped<INotificationHandler<CardTypeRegisteredEvent>, CardTypeEventHandler>();
            services.AddScoped<INotificationHandler<CardTypeUpdatedEvent>, CardTypeEventHandler>();
            services.AddScoped<INotificationHandler<CardTypeRemovedEvent>, CardTypeEventHandler>();

            // Domain - Commands - Card Type
            services.AddScoped<IRequestHandler<RegisterNewCardTypeCommand, bool>, CardTypeCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCardTypeCommand, bool>, CardTypeCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCardTypeCommand, bool>, CardTypeCommandHandler>();

            // Infra - Repository - Card Type
            services.AddScoped<ICardTypeRepository, CardTypeRepository>();

            #endregion
                                                         



            #region - Customer

            // Application Service - Customer
            services.AddScoped<ICustomerAppService, CustomerAppService>();

            // Domain - Events - Customer
            services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerUpdatedEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerRemovedEvent>, CustomerEventHandler>();

            // Domain - Commands - Customer
            services.AddScoped<IRequestHandler<RegisterNewCustomerCommand, bool>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand, bool>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCustomerCommand, bool>, CustomerCommandHandler>();

            // Infra - Repository - Customer
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            #endregion

                              



            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<KarnakContext>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSQLContext>();

            // Infra - Identity Services
            services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            services.AddTransient<ISmsSender, AuthSMSMessageSender>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}