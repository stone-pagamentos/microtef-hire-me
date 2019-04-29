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
    public class CardTypeController : ApiController
    {
        private readonly ICardTypeAppService _cardtypeAppService;

        public CardTypeController(
            ICardTypeAppService cardtypeAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _cardtypeAppService = cardtypeAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("cardtype-management")]
        public IActionResult Get()
        {
            return Response(_cardtypeAppService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("cardtype-management/{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var cardtypeViewModel = _cardtypeAppService.GetById(id);

            return Response(cardtypeViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("cardtype-management/{name}")]
        public IActionResult GetByName(string name)
        {
            var cardtypeViewModel = _cardtypeAppService.GetByName(name);

            return Response(cardtypeViewModel);
        }

        [HttpPost]
        //[Authorize(Policy = "CanWriteCardTypeData")]
        [AllowAnonymous]
        [Route("cardtype-management")]
        public IActionResult Post([FromBody]CardTypeViewModel cardtypeViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(cardtypeViewModel);
            }

            _cardtypeAppService.Register(cardtypeViewModel);

            return Response(cardtypeViewModel);
        }
        
        [HttpPut]
        //[Authorize(Policy = "CanWriteCardTypeData")]
        [AllowAnonymous]
        [Route("cardtype-management")]
        public IActionResult Put([FromBody]CardTypeViewModel cardtypeViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(cardtypeViewModel);
            }

            _cardtypeAppService.Update(cardtypeViewModel);

            return Response(cardtypeViewModel);
        }

        [HttpDelete]
        //[Authorize(Policy = "CanRemoveCardTypeData")]
        [AllowAnonymous]
        [Route("cardtype-management")]
        public IActionResult Delete(Guid id)
        {
            _cardtypeAppService.Remove(id);
            
            return Response();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("cardtype-management/history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var cardtypeHistoryData = _cardtypeAppService.GetAllHistory(id);
            return Response(cardtypeHistoryData);
        }
    }
}
