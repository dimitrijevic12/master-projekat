using CryptoValute.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoValute.DataAccess.CryptoValuteDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<RegisteredWebShop> RegisteredWebShops { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Merchant> Merchants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentType>()
            .HasMany(p => p.RegisteredWebShops)
            .WithMany(p => p.PaymentTypes)
            .UsingEntity<PaymentTypeRegisteredWebShop>(
                j => j
                    .HasOne(pt => pt.RegisteredWebShop)
                    .WithMany(t => t.PaymentTypeRegisteredWebShops)
                    .HasForeignKey(pt => pt.RegisteredWebShopId),
                j => j
                    .HasOne(pt => pt.PaymentType)
                    .WithMany(p => p.PaymentTypeRegisteredWebShops)
                    .HasForeignKey(pt => pt.PaymentTypeId),
                j =>
                {
                    j.HasKey(t => new { t.PaymentTypeId, t.RegisteredWebShopId });
                });
        }
    }
}
