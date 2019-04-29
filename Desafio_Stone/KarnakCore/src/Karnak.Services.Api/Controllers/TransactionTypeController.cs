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
    public class TransactionTypeController : ApiController
    {
        private readonly ITransactionTypeAppService _TransactionTypeAppService;

        public TransactionTypeController(
            ITransactionTypeAppService TransactionTypeAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _TransactionTypeAppService = TransactionTypeAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("TransactionType-management")]
        public IActionResult Get()
        {
            return Response(_TransactionTypeAppService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("TransactionType-management/{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var TransactionTypeViewModel = _TransactionTypeAppService.GetById(id);

            return Response(TransactionTypeViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("TransactionType-management/{name}")]
        public IActionResult GetByName(string name)
        {
            var TransactionTypeViewModel = _TransactionTypeAppService.GetByName(name);

            return Response(TransactionTypeViewModel);
        }

        [HttpPost]
        //[Authorize(Policy = "CanWriteTransactionTypeData")]
        [AllowAnonymous]
        [Route("TransactionType-management")]
        public IActionResult Post([FromBody]TransactionTypeViewModel TransactionTypeViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(TransactionTypeViewModel);
            }

            _TransactionTypeAppService.Register(TransactionTypeViewModel);

            return Response(TransactionTypeViewModel);
        }
        
        [HttpPut]
        //[Authorize(Policy = "CanWriteTransactionTypeData")]
        [AllowAnonymous]
        [Route("TransactionType-management")]
        public IActionResult Put([FromBody]TransactionTypeViewModel TransactionTypeViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(TransactionTypeViewModel);
            }

            _TransactionTypeAppService.Update(TransactionTypeViewModel);

            return Response(TransactionTypeViewModel);
        }

        [HttpDelete]
        //[Authorize(Policy = "CanWriteTransactionTypeData")]
        [AllowAnonymous]
        [Route("TransactionType-management")]
        public IActionResult Delete([FromQuery]Guid id)
        {
            _TransactionTypeAppService.Remove(id);
            
            return Response();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("TransactionType-management/history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var TransactionTypeHistoryData = _TransactionTypeAppService.GetAllHistory(id);
            return Response(TransactionTypeHistoryData);
        }
    }
}
