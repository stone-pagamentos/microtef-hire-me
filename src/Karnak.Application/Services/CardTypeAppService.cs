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
    public class CardTypeAppService : ICardTypeAppService
    {
        private readonly IMapper _mapper;
        private readonly ICardTypeRepository _CardTypeRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public CardTypeAppService(IMapper mapper,
                                  ICardTypeRepository CardTypeRepository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _CardTypeRepository = CardTypeRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<CardTypeViewModel> GetAll()
        {
            return _CardTypeRepository.GetAll().ProjectTo<CardTypeViewModel>(_mapper.ConfigurationProvider);
        }

        public CardTypeViewModel GetById(Guid id)
        {
            return _mapper.Map<CardTypeViewModel>(_CardTypeRepository.GetById(id));
        }

        public CardTypeViewModel GetByName(string name)
        {
            return _mapper.Map<CardTypeViewModel>(_CardTypeRepository.GetByName(name));
        }

        public void Register(CardTypeViewModel CardTypeViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewCardTypeCommand>(CardTypeViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(CardTypeViewModel CardTypeViewModel)
        {
            var updateCommand = _mapper.Map<UpdateCardTypeCommand>(CardTypeViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveCardTypeCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public IList<CardTypeHistoryData> GetAllHistory(Guid id)
        {
            return CardTypeHistory.ToJavaScriptCardTypeHistory(_eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
