using System;
using Karnak.Application.Interfaces;
using Karnak.Application.ViewModels;
using Karnak.Domain.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Karnak.UI.Web.Controllers
{
    [Authorize]
    public class CardBrandController : BaseController
    {
        private readonly ICardBrandAppService _cardBrandAppService;

        public CardBrandController(ICardBrandAppService cardBrandAppService,
                                  INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _cardBrandAppService = cardBrandAppService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("cardBrand-management/list-all")]
        public IActionResult Index()
        {
            return View(_cardBrandAppService.GetAll());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("cardBrand-management/cardBrand-details/{id:guid}")]
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardBrandViewModel = _cardBrandAppService.GetById(id.Value);

            if (cardBrandViewModel == null)
            {
                return NotFound();
            }

            return View(cardBrandViewModel);
        }

        [HttpGet]
        [Authorize(Policy = "CanWriteCardBrandData")]
        [Route("cardBrand-management/register-new")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "CanWriteCardBrandData")]
        [Route("cardBrand-management/register-new")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CardBrandViewModel cardBrandViewModel)
        {
            if (!ModelState.IsValid) return View(cardBrandViewModel);
            _cardBrandAppService.Register(cardBrandViewModel);

            if (IsValidOperation())
                ViewBag.Sucesso = "Card Brand Registered!";

            return View(cardBrandViewModel);
        }

        [HttpGet]
        [Authorize(Policy = "CanWriteCardBrandData")]
        [Route("cardBrand-management/edit-cardBrand/{id:guid}")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardBrandViewModel = _cardBrandAppService.GetById(id.Value);

            if (cardBrandViewModel == null)
            {
                return NotFound();
            }

            return View(cardBrandViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "CanWriteCardBrandData")]
        [Route("cardBrand-management/edit-cardBrand/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CardBrandViewModel cardBrandViewModel)
        {
            if (!ModelState.IsValid) return View(cardBrandViewModel);

            _cardBrandAppService.Update(cardBrandViewModel);

            if (IsValidOperation())
                ViewBag.Sucesso = "Card Brand Updated!";

            return View(cardBrandViewModel);
        }

        [HttpGet]
        [Authorize(Policy = "CanRemoveCardBrandData")]
        [Route("cardBrand-management/remove-cardBrand/{id:guid}")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardBrandViewModel = _cardBrandAppService.GetById(id.Value);

            if (cardBrandViewModel == null)
            {
                return NotFound();
            }

            return View(cardBrandViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "CanRemoveCardBrandData")]
        [Route("cardBrand-management/remove-cardBrand/{id:guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _cardBrandAppService.Remove(id);

            if (!IsValidOperation()) return View(_cardBrandAppService.GetById(id));

            ViewBag.Sucesso = "Card Brand Removed!";
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [Route("cardBrand-management/cardBrand-history/{id:guid}")]
        public JsonResult History(Guid id)
        {
            var cardBrandHistoryData = _cardBrandAppService.GetAllHistory(id);
            return Json(cardBrandHistoryData);
        }
    }
}
