using Karnak.Application.Interfaces;
using Karnak.Application.ViewModels;
using Karnak.Domain.Core.Bus;
using Karnak.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Karnak.Services.Api.Controllers
{
    [Authorize]
    public class CardController : ApiController
    {
        private readonly ICardAppService _CardAppService;

        public CardController(
            ICardAppService CardAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _CardAppService = CardAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Card-management")]
        public IActionResult Get()
        {
            return Response(_CardAppService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Card-management/{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var CardViewModel = _CardAppService.GetById(id);

            return Response(CardViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Card-management/GetByCardNumber/")]
        public IActionResult GetByCardNumber(string cardNumber)
        {
            var CardViewModel = _CardAppService.GetByCardNumber(cardNumber);

            return Response(CardViewModel);
        }

        [HttpPost]
        //[Authorize(Policy = "CanWriteCardData")]
        [AllowAnonymous]
        [Route("Card-management")]
        public IActionResult Post([FromBody]CardViewModel CardViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(CardViewModel);
            }

            _CardAppService.Register(CardViewModel);

            return Response(CardViewModel);
        }
        
        [HttpPut]
        //[Authorize(Policy = "CanWriteCardData")]
        [AllowAnonymous]
        [Route("Card-management")]
        public IActionResult Put([FromBody]CardViewModel CardViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(CardViewModel);
            }

            _CardAppService.Update(CardViewModel);

            return Response(CardViewModel);
        }

        [HttpDelete]
        //[Authorize(Policy = "CanWriteCardData")]
        [AllowAnonymous]
        [Route("Card-management")]
        public IActionResult Delete([FromQuery]Guid id)
        {
            _CardAppService.Remove(id);
            
            return Response();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Card-management/history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var CardHistoryData = _CardAppService.GetAllHistory(id);
            return Response(CardHistoryData);
        }
    }
}
