using System;
using System.Collections.Generic;
using Karnak.Application.EventSourcedNormalizers;
using Karnak.Application.ViewModels;

namespace Karnak.Application.Interfaces
{
    public interface ICardAppService : IDisposable
    {
        void Register(CardViewModel CardViewModel);
        IEnumerable<CardViewModel> GetAll();
        CardViewModel GetById(Guid id);
        CardViewModel GetByCardNumber(string cardNumber);
        void Update(CardViewModel CardViewModel);
        void Remove(Guid id);
        IList<CardHistoryData> GetAllHistory(Guid id);
    }
}
