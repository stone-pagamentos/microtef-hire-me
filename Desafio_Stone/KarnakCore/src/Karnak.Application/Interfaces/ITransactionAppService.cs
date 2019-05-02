using System;
using System.Collections.Generic;
using System.Linq;
using Karnak.Application.EventSourcedNormalizers;
using Karnak.Application.ViewModels;
using Karnak.Domain.Models;

namespace Karnak.Application.Interfaces
{
    public interface ITransactionAppService : IDisposable
    {
        void Register(TransactionViewModel TransactionViewModel);
        IEnumerable<TransactionViewModel> GetAll();
        List<TransactionList> TransactionList();
        List<TransactionList> SondagemTransacoes(string cardNumber);
        TransactionViewModel GetById(Guid id);
        TransactionViewModel GetByName(string name);
        void Update(TransactionViewModel TransactionViewModel);
        void Remove(Guid id);
        IList<TransactionHistoryData> GetAllHistory(Guid id);
    }
}
