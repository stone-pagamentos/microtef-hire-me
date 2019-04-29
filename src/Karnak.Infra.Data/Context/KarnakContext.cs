using Karnak.Domain.Models;
using Karnak.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Karnak.Infra.Data.Context
{
    public class KarnakContext : DbContext
    {
        private readonly IHostingEnvironment _env;

        public KarnakContext(IHostingEnvironment env)
        {
            _env = env;
        }

        public DbSet<TransactionType> TransactionType { get; set; }

        public DbSet<TransactionStatus> TransactionStatus { get; set; }

        public DbSet<Transaction> Transaction { get; set; }

        public DbSet<CardBrand> CardBrand { get; set; }

        public DbSet<CardType> CardType { get; set; }

        public DbSet<Card> Card { get; set; }      

        public DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransactionTypeMap());

            modelBuilder.ApplyConfiguration(new TransactionStatusMap());

            modelBuilder.ApplyConfiguration(new TransactionMap());

            modelBuilder.ApplyConfiguration(new CardBrandMap());

            modelBuilder.ApplyConfiguration(new CardTypeMap());

            modelBuilder.ApplyConfiguration(new CardMap());            

            modelBuilder.ApplyConfiguration(new CustomerMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();            
            
            // define the database to use
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }
}
