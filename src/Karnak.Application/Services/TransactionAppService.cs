using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Karnak.Application.EventSourcedNormalizers;
using Karnak.Application.Interfaces;
using Karnak.Application.ViewModels;
using Karnak.Domain.Commands;
using Karnak.Domain.Core.Bus;
using Karnak.Domain.Interfaces;
using Karnak.Domain.Models;
using Karnak.Infra.Data.Repository.EventSourcing;

namespace Karnak.Application.Services
{
    public class TransactionAppService : ITransactionAppService
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _TransactionRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public TransactionAppService(IMapper mapper,
                                  ICardRepository CardRepository,
                                  ITransactionRepository TransactionRepository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _TransactionRepository = TransactionRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<TransactionViewModel> GetAll()
        {
            return _TransactionRepository.GetAll().ProjectTo<TransactionViewModel>(_mapper.ConfigurationProvider);
        }

        public List<TransactionList> TransactionList()
        {
            List<TransactionList> retorno = _TransactionRepository.TransactionList();

            return retorno;
        }

        public List<TransactionList> SondagemTransacoes(string cardNumber)
        {
            List<TransactionList> retorno = _TransactionRepository.SondagemTransacoes(cardNumber);

            return retorno;
        }

        public TransactionViewModel GetById(Guid id)
        {
            return _mapper.Map<TransactionViewModel>(_TransactionRepository.GetById(id));
        }

        public TransactionViewModel GetByName(string name)
        {
            return _mapper.Map<TransactionViewModel>(_TransactionRepository.GetByName(name));
        }

        public void Register(TransactionViewModel TransactionViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewTransactionCommand>(TransactionViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(TransactionViewModel TransactionViewModel)
        {
            var updateCommand = _mapper.Map<UpdateTransactionCommand>(TransactionViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveTransactionCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public IList<TransactionHistoryData> GetAllHistory(Guid id)
        {
            return TransactionHistory.ToJavaScriptTransactionHistory(_eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
