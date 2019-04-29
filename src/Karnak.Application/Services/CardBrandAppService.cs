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
    public class CardBrandAppService : ICardBrandAppService
    {
        private readonly IMapper _mapper;
        private readonly ICardBrandRepository _CardBrandRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public CardBrandAppService(IMapper mapper,
                                  ICardBrandRepository CardBrandRepository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _CardBrandRepository = CardBrandRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<CardBrandViewModel> GetAll()
        {
            return _CardBrandRepository.GetAll().ProjectTo<CardBrandViewModel>(_mapper.ConfigurationProvider);
        }

        public CardBrandViewModel GetById(Guid id)
        {
            return _mapper.Map<CardBrandViewModel>(_CardBrandRepository.GetById(id));
        }

        public CardBrandViewModel GetByName(string name)
        {
            return _mapper.Map<CardBrandViewModel>(_CardBrandRepository.GetByName(name));
        }

        public void Register(CardBrandViewModel CardBrandViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewCardBrandCommand>(CardBrandViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(CardBrandViewModel CardBrandViewModel)
        {
            var updateCommand = _mapper.Map<UpdateCardBrandCommand>(CardBrandViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveCardBrandCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public IList<CardBrandHistoryData> GetAllHistory(Guid id)
        {
            return CardBrandHistory.ToJavaScriptCardBrandHistory(_eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
