using Karnak.Infra.CrossCutting.Identity.Authorization;
using Karnak.Infra.CrossCutting.Identity.Data;
using Karnak.Infra.CrossCutting.Identity.Models;
using Karnak.Infra.CrossCutting.IoC;
using Karnak.Services.Api.Configurations;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Karnak.Services.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);
                         
            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                options.UseCentralRoutePrefix(new RouteAttribute("api/v{version}"));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAutoMapperSetup();

            services.AddAuthorization(options =>
            {
                // Transaction Type
                options.AddPolicy("CanWriteTransactionTypeData", policy => policy.Requirements.Add(new ClaimRequirement("TransactionTypes", "Write")));
                options.AddPolicy("CanRemoveTransactionTypeData", policy => policy.Requirements.Add(new ClaimRequirement("TransactionTypes", "Remove")));

                // Transaction Status
                options.AddPolicy("CanWriteTransactionStatusData", policy => policy.Requirements.Add(new ClaimRequirement("TransactionStatus", "Write")));
                options.AddPolicy("CanRemoveTransactionStatusData", policy => policy.Requirements.Add(new ClaimRequirement("TransactionStatus", "Remove")));

                // Transaction
                options.AddPolicy("CanWriteTransactionData", policy => policy.Requirements.Add(new ClaimRequirement("Transactions", "Write")));
                options.AddPolicy("CanRemoveTransactionData", policy => policy.Requirements.Add(new ClaimRequirement("Transactions", "Remove")));

                // Card Brand
                options.AddPolicy("CanWriteCardBrandData", policy => policy.Requirements.Add(new ClaimRequirement("CardBrands", "Write")));
                options.AddPolicy("CanRemoveCardBrandData", policy => policy.Requirements.Add(new ClaimRequirement("CardBrands", "Remove")));

                // Card Type
                options.AddPolicy("CanWriteCardTypeData", policy => policy.Requirements.Add(new ClaimRequirement("CardTypes", "Write")));
                options.AddPolicy("CanRemoveCardTypeData", policy => policy.Requirements.Add(new ClaimRequirement("CardTypes", "Remove")));

                // Card
                options.AddPolicy("CanWriteCardData", policy => policy.Requirements.Add(new ClaimRequirement("Cards", "Write")));
                options.AddPolicy("CanRemoveCardData", policy => policy.Requirements.Add(new ClaimRequirement("Cards", "Remove")));

                // Customer
                options.AddPolicy("CanWriteCustomerData", policy => policy.Requirements.Add(new ClaimRequirement("Customers", "Write")));
                options.AddPolicy("CanRemoveCustomerData", policy => policy.Requirements.Add(new ClaimRequirement("Customers", "Remove")));
            });

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Karnak Project",
                    Description = "Karnak API Swagger surface",
                    Contact = new Contact { Name = "Stefan Robinson da Silva", Email = "stefansilva@brq.com", Url = "http://www.brq.com.br" },
                    License = new License { Name = "MIT", Url = "https://github.com/silvastefan/microtef-hire-me/Desafio/LICENSE" }
                });
            });

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));

            // .NET Native DI Abstraction
            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Karnak Project API v1.1");
            });
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
