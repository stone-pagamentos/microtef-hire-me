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
    public class TransactionStatusController : ApiController
    {
        private readonly ITransactionStatusAppService _TransactionStatusAppService;

        public TransactionStatusController(
            ITransactionStatusAppService TransactionStatusAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _TransactionStatusAppService = TransactionStatusAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("TransactionStatus-management")]
        public IActionResult Get()
        {
            return Response(_TransactionStatusAppService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("TransactionStatus-management/{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var TransactionStatusViewModel = _TransactionStatusAppService.GetById(id);

            return Response(TransactionStatusViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("TransactionStatus-management/{name}")]
        public IActionResult GetByName(string name)
        {
            var TransactionStatusViewModel = _TransactionStatusAppService.GetByName(name);

            return Response(TransactionStatusViewModel);
        }

        [HttpPost]
        //[Authorize(Policy = "CanWriteTransactionStatusData")]
        [AllowAnonymous]
        [Route("TransactionStatus-management")]
        public IActionResult Post([FromBody]TransactionStatusViewModel TransactionStatusViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(TransactionStatusViewModel);
            }

            _TransactionStatusAppService.Register(TransactionStatusViewModel);

            return Response(TransactionStatusViewModel);
        }
        
        [HttpPut]
        //[Authorize(Policy = "CanWriteTransactionStatusData")]
        [AllowAnonymous]
        [Route("TransactionStatus-management")]
        public IActionResult Put([FromBody]TransactionStatusViewModel TransactionStatusViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(TransactionStatusViewModel);
            }

            _TransactionStatusAppService.Update(TransactionStatusViewModel);

            return Response(TransactionStatusViewModel);
        }

        [HttpDelete]
        //[Authorize(Policy = "CanWriteTransactionStatusData")]
        [AllowAnonymous]
        [Route("TransactionStatus-management")]
        public IActionResult Delete([FromQuery]Guid id)
        {
            _TransactionStatusAppService.Remove(id);
            
            return Response();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("TransactionStatus-management/history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var TransactionStatusHistoryData = _TransactionStatusAppService.GetAllHistory(id);
            return Response(TransactionStatusHistoryData);
        }
    }
}
