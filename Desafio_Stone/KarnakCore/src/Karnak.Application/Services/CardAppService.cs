using AutoMapper;
using AutoMapper.QueryableExtensions;
using Karnak.Application.EventSourcedNormalizers;
using Karnak.Application.Interfaces;
using Karnak.Application.ViewModels;
using Karnak.Domain.Commands;
using Karnak.Domain.Core.Bus;
using Karnak.Domain.Interfaces;
using Karnak.Infra.Data.Repository.EventSourcing;
using System;
using System.Collections.Generic;

namespace Karnak.Application.Services
{
    public class CardAppService : ICardAppService
    {
        private readonly IMapper _mapper;
        private readonly ICardRepository _CardRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public CardAppService(IMapper mapper,
                                  ICardRepository CardRepository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _CardRepository = CardRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<CardViewModel> GetAll()
        {
            return _CardRepository.GetAll().ProjectTo<CardViewModel>(_mapper.ConfigurationProvider);
        }

        public CardViewModel GetById(Guid id)
        {
            return _mapper.Map<CardViewModel>(_CardRepository.GetById(id));
        }

        public CardViewModel GetByCardNumber(string cardNumber)
        {
            return _mapper.Map<CardViewModel>(_CardRepository.GetByCardNumber(cardNumber));
        }

        public void Register(CardViewModel CardViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewCardCommand>(CardViewModel);
            Bus.SendCommand(registerCommand);
        }

        public void Update(CardViewModel CardViewModel)
        {
            var updateCommand = _mapper.Map<UpdateCardCommand>(CardViewModel);
            Bus.SendCommand(updateCommand);
        }

        public void Remove(Guid id)
        {
            var removeCommand = new RemoveCardCommand(id);
            Bus.SendCommand(removeCommand);
        }

        public IList<CardHistoryData> GetAllHistory(Guid id)
        {
            return CardHistory.ToJavaScriptCardHistory(_eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
