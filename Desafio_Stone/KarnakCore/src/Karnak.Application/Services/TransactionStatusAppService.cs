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
    public class TransactionStatusAppService : ITransactionStatusAppService
    {
        private readonly IMapper _mapper;
        private readonly ITransactionStatusRepository _TransactionStatusRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public TransactionStatusAppService(IMapper mapper,
                                  ITransactionStatusRepository TransactionStatusRepository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _TransactionStatusRepository = TransactionStatusRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<TransactionStatusViewModel> GetAll()
        {
            return _TransactionStatusRepository.GetAll().ProjectTo<TransactionStatusViewModel>(_mapper.ConfigurationProvider);
        }

        public TransactionStatusViewModel GetById(Guid id)
        {
            return _mapper.Map<TransactionStatusViewModel>(_TransactionStatusRepository.GetById(id));
        }

        public TransactionStatusViewModel GetByName(string name)
        {
            return _mapper.Map<TransactionStatusViewModel>(_TransactionStatusRepository.GetByName(name));
        }

        public void Register(TransactionStatusViewModel TransactionStatusViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewTransactionStatusCommand>(TransactionStatusViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(TransactionStatusViewModel TransactionStatusViewModel)
        {
            var updateCommand = _mapper.Map<UpdateTransactionStatusCommand>(TransactionStatusViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveTransactionStatusCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public IList<TransactionStatusHistoryData> GetAllHistory(Guid id)
        {
            return TransactionStatusHistory.ToJavaScriptTransactionStatusHistory(_eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
