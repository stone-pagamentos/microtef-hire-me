using System;
using System.Collections.Generic;
using Karnak.Application.EventSourcedNormalizers;
using Karnak.Application.ViewModels;

namespace Karnak.Application.Interfaces
{
    public interface ICustomerAppService : IDisposable
    {
        void Register(CustomerViewModel customerViewModel);
        IEnumerable<CustomerViewModel> GetAll();
        CustomerViewModel GetById(Guid id);
        CustomerViewModel GetByName(string name);
        void Update(CustomerViewModel customerViewModel);
        void Remove(Guid id);
        IList<CustomerHistoryData> GetAllHistory(Guid id);
    }
}
