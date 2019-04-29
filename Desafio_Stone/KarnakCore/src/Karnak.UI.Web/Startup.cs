using Karnak.Infra.CrossCutting.Identity.Authorization;
using Karnak.Infra.CrossCutting.Identity.Data;
using Karnak.Infra.CrossCutting.Identity.Models;
using Karnak.Infra.CrossCutting.IoC;
using Karnak.UI.Web.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Karnak.UI.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o => {
                    o.LoginPath = new PathString("/login");
                    o.AccessDeniedPath = new PathString("/home/access-denied");
                })
                .AddFacebook(o =>
                {
                    o.AppId = Configuration["Authentication:Facebook:AppId"];
                    o.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                })
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAutoMapperSetup();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanWriteCustomerData", policy => policy.Requirements.Add(new ClaimRequirement("Customers", "Write")));
                options.AddPolicy("CanRemoveCustomerData", policy => policy.Requirements.Add(new ClaimRequirement("Customers", "Remove")));

                options.AddPolicy("CanWriteCardBrandData", policy => policy.Requirements.Add(new ClaimRequirement("CardBrand", "Write")));
                options.AddPolicy("CanRemoveCardBrandData", policy => policy.Requirements.Add(new ClaimRequirement("CardBrand", "Remove")));

                options.AddPolicy("CanWriteTransactionTypeData", policy => policy.Requirements.Add(new ClaimRequirement("TransactionTypes", "Write")));
                options.AddPolicy("CanRemoveTransactionTypeData", policy => policy.Requirements.Add(new ClaimRequirement("TransactionType", "Remove")));

                options.AddPolicy("CanWriteCardTypeData", policy => policy.Requirements.Add(new ClaimRequirement("CardTypes", "Write")));
                options.AddPolicy("CanRemoveCardTypeData", policy => policy.Requirements.Add(new ClaimRequirement("CardTypes", "Remove")));
            });

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));

            // .NET Native DI Abstraction
            RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
