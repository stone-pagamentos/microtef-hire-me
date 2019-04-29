using System;
using System.Collections.Generic;
using Karnak.Application.EventSourcedNormalizers;
using Karnak.Application.ViewModels;

namespace Karnak.Application.Interfaces
{
    public interface ICardBrandAppService : IDisposable
    {
        void Register(CardBrandViewModel CardBrandViewModel);
        IEnumerable<CardBrandViewModel> GetAll();
        CardBrandViewModel GetById(Guid id);
        CardBrandViewModel GetByName(string name);
        void Update(CardBrandViewModel CardBrandViewModel);
        void Remove(Guid id);
        IList<CardBrandHistoryData> GetAllHistory(Guid id);
    }
}
