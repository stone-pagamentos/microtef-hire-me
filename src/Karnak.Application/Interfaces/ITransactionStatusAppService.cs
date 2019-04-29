using System;
using System.Collections.Generic;
using Karnak.Application.EventSourcedNormalizers;
using Karnak.Application.ViewModels;

namespace Karnak.Application.Interfaces
{
    public interface ITransactionStatusAppService : IDisposable
    {
        void Register(TransactionStatusViewModel TransactionStatusViewModel);
        IEnumerable<TransactionStatusViewModel> GetAll();
        TransactionStatusViewModel GetById(Guid id);
        TransactionStatusViewModel GetByName(string name);
        void Update(TransactionStatusViewModel TransactionStatusViewModel);
        void Remove(Guid id);
        IList<TransactionStatusHistoryData> GetAllHistory(Guid id);
    }
}
