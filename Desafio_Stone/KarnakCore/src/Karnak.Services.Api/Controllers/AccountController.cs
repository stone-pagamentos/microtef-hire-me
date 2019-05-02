using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Karnak.Domain.Core.Bus;
using Karnak.Domain.Core.Notifications;
using Karnak.Infra.CrossCutting.Identity.Data;
using Karnak.Infra.CrossCutting.Identity.Models;
using Karnak.Infra.CrossCutting.Identity.Models.AccountViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Karnak.Services.Api.Controllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            INotificationHandler<DomainNotification> notifications,
            ILoggerFactory loggerFactory,
            IMediatorHandler mediator) : base(notifications, mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("account")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }

            var userClaim = await _userManager.GetUserAsync(HttpContext.User);

            // User claim for write / remove  - TransactionStatus
            await _userManager.AddClaimAsync(userClaim, new Claim("TransactionStatus", "Write"));
            await _userManager.AddClaimAsync(userClaim, new Claim("TransactionStatus", "Remove"));

            // User claim for write / remove  - Transactions
            await _userManager.AddClaimAsync(userClaim, new Claim("Transactions", "Write"));
            await _userManager.AddClaimAsync(userClaim, new Claim("Transactions", "Remove"));

            // User claim for write / remove  - CardBrands
            await _userManager.AddClaimAsync(userClaim, new Claim("CardBrands", "Write"));
            await _userManager.AddClaimAsync(userClaim, new Claim("CardBrands", "Remove"));

            // User claim for write / remove  - CardTypes
            await _userManager.AddClaimAsync(userClaim, new Claim("CardTypes", "Write"));
            await _userManager.AddClaimAsync(userClaim, new Claim("CardTypes", "Remove"));

            // User claim for write / remove  - Cards
            await _userManager.AddClaimAsync(userClaim, new Claim("Cards", "Write"));
            await _userManager.AddClaimAsync(userClaim, new Claim("Cards", "Remove"));

            // User claim for write / remove  - Customers
            await _userManager.AddClaimAsync(userClaim, new Claim("Customers", "Write"));
            await _userManager.AddClaimAsync(userClaim, new Claim("Customers", "Remove"));

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);
            if (!result.Succeeded)
            {
                NotifyError(result.ToString(), "Login failure");
            }
            else
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                // User claim for write / remove  - TransactionStatus
                await _userManager.AddClaimAsync(user, new Claim("TransactionStatus", "Write"));
                await _userManager.AddClaimAsync(user, new Claim("TransactionStatus", "Remove"));

                // User claim for write / remove  - Transactions
                await _userManager.AddClaimAsync(user, new Claim("Transactions", "Write"));
                await _userManager.AddClaimAsync(user, new Claim("Transactions", "Remove"));

                // User claim for write / remove  - CardBrands
                await _userManager.AddClaimAsync(user, new Claim("CardBrands", "Write"));
                await _userManager.AddClaimAsync(user, new Claim("CardBrands", "Remove"));

                // User claim for write / remove  - CardTypes
                await _userManager.AddClaimAsync(user, new Claim("CardTypes", "Write"));
                await _userManager.AddClaimAsync(user, new Claim("CardTypes", "Remove"));

                // User claim for write / remove  - Cards
                await _userManager.AddClaimAsync(user, new Claim("Cards", "Write"));
                await _userManager.AddClaimAsync(user, new Claim("Cards", "Remove"));

                // User claim for write / remove  - Customers
                await _userManager.AddClaimAsync(user, new Claim("Customers", "Write"));
                await _userManager.AddClaimAsync(user, new Claim("Customers", "Remove"));

                _logger.LogInformation(1, "User logged in.");
            }

            return Response(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("account/register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(model);
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // User claim for write / remove  - TransactionStatus
                await _userManager.AddClaimAsync(user, new Claim("TransactionStatus", "Write"));
                await _userManager.AddClaimAsync(user, new Claim("TransactionStatus", "Remove"));

                // User claim for write / remove  - Transactions
                await _userManager.AddClaimAsync(user, new Claim("Transactions", "Write"));
                await _userManager.AddClaimAsync(user, new Claim("Transactions", "Remove"));

                // User claim for write / remove  - CardBrands
                await _userManager.AddClaimAsync(user, new Claim("CardBrands", "Write"));
                await _userManager.AddClaimAsync(user, new Claim("CardBrands", "Remove"));

                // User claim for write / remove  - CardTypes
                await _userManager.AddClaimAsync(user, new Claim("CardTypes", "Write"));
                await _userManager.AddClaimAsync(user, new Claim("CardTypesola", "Remove"));

                // User claim for write / remove  - Cards
                await _userManager.AddClaimAsync(user, new Claim("Cards", "Write"));
                await _userManager.AddClaimAsync(user, new Claim("Cards", "Remove"));

                // User claim for write / remove  - Customers
                await _userManager.AddClaimAsync(user, new Claim("Customers", "Write"));
                await _userManager.AddClaimAsync(user, new Claim("Customers", "Remove"));

                await _signInManager.SignInAsync(user, false);
                _logger.LogInformation(3, "User created a new account with password.");
                return Response(model);
            }

            AddIdentityErrors(result);
            return Response(model);
        }
    }
}
