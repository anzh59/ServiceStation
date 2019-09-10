using ServiceStation.Domain.Entities;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using ServiceStation.Domain.Abstract;

namespace ServiceStation.Domain.Implementation
{
    public class EFDbContext : DbContext
    {
        private IDBAccessor _dbAccessor;
        public EFDbContext(IDBAccessor dbAccessor)
        {
            _dbAccessor = dbAccessor;
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderService> OrderService { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderService>()
                .HasKey(os => new { os.OrderId, os.ServiceTypeId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_dbAccessor.ConnectionString);
        }
    }
}
