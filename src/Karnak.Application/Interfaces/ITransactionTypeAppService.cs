using System;
using System.Collections.Generic;
using Karnak.Application.EventSourcedNormalizers;
using Karnak.Application.ViewModels;

namespace Karnak.Application.Interfaces
{
    public interface ITransactionTypeAppService : IDisposable
    {
        void Register(TransactionTypeViewModel TransactionTypeViewModel);
        IEnumerable<TransactionTypeViewModel> GetAll();
        TransactionTypeViewModel GetById(Guid id);
        TransactionTypeViewModel GetByName(string name);
        void Update(TransactionTypeViewModel TransactionTypeViewModel);
        void Remove(Guid id);
        IList<TransactionTypeHistoryData> GetAllHistory(Guid id);
    }
}
