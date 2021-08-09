using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using FuelControl.Entities;

namespace FuelControl.Helpers
{
    public class DataContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<FuelSupply> FuelSupplies { get; set; }
        public DbSet<FuelPrice> FuelPrices { get; set; }
        
        private readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("FuelControlDatabase"));
        }
    }
}