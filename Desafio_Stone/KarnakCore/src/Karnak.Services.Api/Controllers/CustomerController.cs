using System;
using Karnak.Application.Interfaces;
using Karnak.Application.ViewModels;
using Karnak.Domain.Core.Bus;
using Karnak.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Karnak.Services.Api.Controllers
{
    [Authorize]
    public class CustomerController : ApiController
    {
        private readonly ICustomerAppService _customerAppService;

        public CustomerController(
            ICustomerAppService customerAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _customerAppService = customerAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("customer-management")]
        public IActionResult Get()
        {
            return Response(_customerAppService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("customer-management/{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var customerViewModel = _customerAppService.GetById(id);

            return Response(customerViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("customer-management/{name}")]
        public IActionResult GetByName(string name)
        {
            var customerViewModel = _customerAppService.GetByName(name);

            return Response(customerViewModel);
        }

        [HttpPost]
        //[Authorize(Policy = "CanWriteCustomerData")]
        [AllowAnonymous]
        [Route("customer-management")]
        public IActionResult Post([FromBody]CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(customerViewModel);
            }

            _customerAppService.Register(customerViewModel);

            return Response(customerViewModel);
        }
        
        [HttpPut]
        //[Authorize(Policy = "CanWriteCustomerData")]
        [AllowAnonymous]
        [Route("customer-management")]
        public IActionResult Put([FromBody]CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(customerViewModel);
            }

            _customerAppService.Update(customerViewModel);

            return Response(customerViewModel);
        }

        [HttpDelete]
        //[Authorize(Policy = "CanWriteCustomerData")]
        [AllowAnonymous]
        [Route("customer-management")]
        public IActionResult Delete([FromQuery]Guid id)
        {
            _customerAppService.Remove(id);
            
            return Response();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("customer-management/history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var customerHistoryData = _customerAppService.GetAllHistory(id);
            return Response(customerHistoryData);
        }
    }
}
