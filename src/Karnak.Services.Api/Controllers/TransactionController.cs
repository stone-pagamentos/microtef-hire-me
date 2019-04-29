using System;
using System.Linq;
using Karnak.Application.Interfaces;
using Karnak.Application.ViewModels;
using Karnak.Domain.Core.Bus;
using Karnak.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Karnak.Services.Api.Controllers
{
    [Authorize]
    public class TransactionController : ApiController
    {
        private readonly ITransactionAppService _TransactionAppService;

        public TransactionController(
            ITransactionAppService TransactionAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _TransactionAppService = TransactionAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Transaction-management")]
        public IActionResult Get()
        {
            return Response(_TransactionAppService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Transaction-management/transactionList/")]
        public IActionResult TransactionList()
        {
            dynamic TransactionList = _TransactionAppService.TransactionList();

            return Response(TransactionList);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Transaction-management/SondagemTransacoes/")]
        public IActionResult SondagemTransacoes(string cardNumber)
        {
            dynamic TransactionList = _TransactionAppService.SondagemTransacoes(cardNumber);

            return Response(TransactionList);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Transaction-management/{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var TransactionViewModel = _TransactionAppService.GetById(id);

            return Response(TransactionViewModel);
        }     

        [HttpPost]
        //[Authorize(Policy = "CanWriteTransactionData")]
        [AllowAnonymous]
        [Route("Transaction-management")]
        public IActionResult Post([FromBody]TransactionViewModel TransactionViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(TransactionViewModel);
            }

            _TransactionAppService.Register(TransactionViewModel);

            return Response(TransactionViewModel);
        }
        
        [HttpPut]
        //[Authorize(Policy = "CanWriteTransactionData")]
        [AllowAnonymous]
        [Route("Transaction-management")]
        public IActionResult Put([FromBody]TransactionViewModel TransactionViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(TransactionViewModel);
            }

            _TransactionAppService.Update(TransactionViewModel);

            return Response(TransactionViewModel);
        }

        [HttpDelete]
        //[Authorize(Policy = "CanWriteTransactionData")]
        [AllowAnonymous]
        [Route("Transaction-management")]
        public IActionResult Delete([FromQuery]Guid id)
        {
            _TransactionAppService.Remove(id);
            
            return Response();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Transaction-management/history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var TransactionHistoryData = _TransactionAppService.GetAllHistory(id);
            return Response(TransactionHistoryData);
        }
    }
}
