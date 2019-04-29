using Karnak.Domain.Models;
using System;
using System.Collections.Generic;

namespace Karnak.Domain.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Transaction GetByName(string name);

        List<TransactionList> TransactionList();

        List<TransactionList> SondagemTransacoes(string cardNumber);
    }
}