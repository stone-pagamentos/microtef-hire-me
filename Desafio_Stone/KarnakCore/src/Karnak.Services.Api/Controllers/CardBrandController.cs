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
    public class CardBrandController : ApiController
    {
        private readonly ICardBrandAppService _CardBrandAppService;

        public CardBrandController(
            ICardBrandAppService CardBrandAppService,
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _CardBrandAppService = CardBrandAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("CardBrand-management")]
        public IActionResult Get()
        {
            return Response(_CardBrandAppService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("CardBrand-management/{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var CardBrandViewModel = _CardBrandAppService.GetById(id);

            return Response(CardBrandViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("CardBrand-management/{name}")]
        public IActionResult GetByName(string name)
        {
            var CardBrandViewModel = _CardBrandAppService.GetByName(name);

            return Response(CardBrandViewModel);
        }

        [HttpPost]
        //[Authorize(Policy = "CanWriteCardBrandData")]
        [AllowAnonymous]
        [Route("CardBrand-management")]
        public IActionResult Post([FromBody]CardBrandViewModel CardBrandViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(CardBrandViewModel);
            }

            _CardBrandAppService.Register(CardBrandViewModel);

            return Response(CardBrandViewModel);
        }
        
        [HttpPut]
        //[Authorize(Policy = "CanWriteCardBrandData")]
        [AllowAnonymous]
        [Route("CardBrand-management")]
        public IActionResult Put([FromBody]CardBrandViewModel CardBrandViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(CardBrandViewModel);
            }

            _CardBrandAppService.Update(CardBrandViewModel);

            return Response(CardBrandViewModel);
        }

        [HttpDelete]
        //[Authorize(Policy = "CanWriteCardBrandData")]
        [AllowAnonymous]
        [Route("CardBrand-management")]
        public IActionResult Delete([FromQuery]Guid id)
        {
            _CardBrandAppService.Remove(id);

            return Response();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("CardBrand-management/history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var CardBrandHistoryData = _CardBrandAppService.GetAllHistory(id);
            return Response(CardBrandHistoryData);
        }
    }
}
