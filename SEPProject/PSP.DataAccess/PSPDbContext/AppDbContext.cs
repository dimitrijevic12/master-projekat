using Microsoft.EntityFrameworkCore;
using PSP.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.DataAccess.PSPDbContext
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

            PaymentType paymentTypePayPal = new PaymentType(new Guid("12345678-1234-1234-1234-123412341234"), "PayPal");

            PaymentType paymentTypeCrypto = new PaymentType(new Guid("12345678-1234-1234-1234-223412341234"), "CryptoValute");

            PaymentType paymentTypeBank = new PaymentType(new Guid("12345678-1234-1234-1234-323412341234"), "Bank");


            modelBuilder.Entity<PaymentType>().HasData(paymentTypePayPal, paymentTypeCrypto, paymentTypeBank);

            modelBuilder.Entity<RegisteredWebShop>().HasData(
               new RegisteredWebShop(new Guid("12345678-1234-1234-1234-123412341230"), 123, "WebShopName", "password", "gmail@gmail.com",
               new System.Uri("http://farm4.static.flickr.com/2232/2232/someimage.jpg"), new System.Uri("http://farm4.static.flickr.com/2232/2232/someimage.jpg"),
               new System.Uri("http://farm4.static.flickr.com/2232/2232/someimage.jpg"))
            );

            modelBuilder.Entity<PaymentTypeRegisteredWebShop>().HasData(
                new PaymentTypeRegisteredWebShop(new Guid("12345678-1234-1234-1234-123412341234"), new Guid("12345678-1234-1234-1234-123412341230")),
                new PaymentTypeRegisteredWebShop(new Guid("12345678-1234-1234-1234-223412341234"), new Guid("12345678-1234-1234-1234-123412341230")),
                new PaymentTypeRegisteredWebShop(new Guid("12345678-1234-1234-1234-323412341234"), new Guid("12345678-1234-1234-1234-123412341230"))
             );

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction(new Guid("12345678-1234-1234-1234-123412341234"), 100.0, DateTime.Now, new Guid("12345678-1234-1234-1234-123412341232"), TransactionStatus.Pending,
                new Guid("12345678-1234-1234-1234-123412341233"), "MerchantName", new Guid("12345678-1234-1234-1234-123412341235"), "IssuerName")
             );

            modelBuilder.Entity<Merchant>().HasData(
              new Merchant(new Guid("12345678-1234-1234-1234-123422941234"), new Guid("12345678-1234-1234-1234-123422641234"),"Password", "Name", new Guid("12345678-1234-1234-1234-123412341230"))
           );
        }
    }
}