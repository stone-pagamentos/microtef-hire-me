using System;
using System.Collections.Generic;
using Karnak.Application.EventSourcedNormalizers;
using Karnak.Application.ViewModels;

namespace Karnak.Application.Interfaces
{
    public interface ICardTypeAppService : IDisposable
    {
        void Register(CardTypeViewModel CardTypeViewModel);
        IEnumerable<CardTypeViewModel> GetAll();
        CardTypeViewModel GetById(Guid id);
        CardTypeViewModel GetByName(string name);
        void Update(CardTypeViewModel CardTypeViewModel);
        void Remove(Guid id);
        IList<CardTypeHistoryData> GetAllHistory(Guid id);
    }
}
