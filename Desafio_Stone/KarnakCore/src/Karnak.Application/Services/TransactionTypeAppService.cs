using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Karnak.Application.EventSourcedNormalizers;
using Karnak.Application.Interfaces;
using Karnak.Application.ViewModels;
using Karnak.Domain.Commands;
using Karnak.Domain.Core.Bus;
using Karnak.Domain.Interfaces;
using Karnak.Infra.Data.Repository.EventSourcing;

namespace Karnak.Application.Services
{
    public class TransactionTypeAppService : ITransactionTypeAppService
    {
        private readonly IMapper _mapper;
        private readonly ITransactionTypeRepository _TransactionTypeRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public TransactionTypeAppService(IMapper mapper,
                                  ITransactionTypeRepository TransactionTypeRepository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _TransactionTypeRepository = TransactionTypeRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<TransactionTypeViewModel> GetAll()
        {
            return _TransactionTypeRepository.GetAll().ProjectTo<TransactionTypeViewModel>(_mapper.ConfigurationProvider);
        }

        public TransactionTypeViewModel GetById(Guid id)
        {
            return _mapper.Map<TransactionTypeViewModel>(_TransactionTypeRepository.GetById(id));
        }

        public TransactionTypeViewModel GetByName(string name)
        {
            return _mapper.Map<TransactionTypeViewModel>(_TransactionTypeRepository.GetByName(name));
        }

        public void Register(TransactionTypeViewModel TransactionTypeViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewTransactionTypeCommand>(TransactionTypeViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(TransactionTypeViewModel TransactionTypeViewModel)
        {
            var updateCommand = _mapper.Map<UpdateTransactionTypeCommand>(TransactionTypeViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveTransactionTypeCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public IList<TransactionTypeHistoryData> GetAllHistory(Guid id)
        {
            return TransactionTypeHistory.ToJavaScriptTransactionTypeHistory(_eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
