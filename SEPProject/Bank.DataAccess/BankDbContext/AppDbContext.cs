using Bank.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DataAccess.BankDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<PaymentCard> PaymentCards{ get; set; }
        public DbSet<PSPRequest> PSPRequests { get; set; }
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Merchant merchant = new Merchant(new Guid("12345678-1234-1234-1234-123412341235"), new Guid("12345678-1234-1234-1234-123412341232"), "password");
            modelBuilder.Entity<Merchant>().HasData(
                merchant
             );

            RegisteredUser registeredUser = new RegisteredUser(new Guid("12345678-1234-1234-1234-123412341234"), "FirstName", "LastName", new DateTime(1990, 4, 4),
                "user@gmail.com", "123456789");
            modelBuilder.Entity<RegisteredUser>().HasData(
                registeredUser
             );

            modelBuilder.Entity<Account>().HasData(
                new Account(new Guid("12345678-1234-1234-1234-123412341234"), "123456789", 100000.0, 
                            new Guid("12345678-1234-1234-1234-123412341235")),
                new Account(new Guid("12345678-1234-1234-1234-123412341235"), "222222222", 222222.0, 
                            new Guid("12345678-1234-1234-1234-123412341234"))
             );

            modelBuilder.Entity<PaymentCard>().HasData(
                new PaymentCard(new Guid("12345678-1234-1234-1234-123412341234"), "123456789", "1234", "Holder Name", "4/22",
                                new Guid("12345678-1234-1234-1234-123412341234"))
             );

            modelBuilder.Entity<PSPRequest>().HasData(
                new PSPRequest(new Guid("12345678-1234-1234-1234-123412341234"),
                                new Guid("12345678-1234-1234-1234-123412341235"), "password", 123.0, 
                                new Guid("12345678-1234-1234-1234-123412341234"), DateTime.Now, new Uri("https://www.webshop.com/success"),
                                new Uri("https://www.webshop.com/failure"), new Uri("https://www.webshop.com/error"))
             );

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction(new Guid("12345678-1234-1234-1234-123412341234"), 444.0, DateTime.Now, 
                                new Guid("12345678-1234-1234-1234-123412341234"), TransactionStatus.Success, 
                                new Guid("12345678-1234-1234-1234-123412341235"), "Acquirer name", 
                                new Guid("12345678-1234-1234-1234-123412341234"), "Issuer name")
             );
        }
    }
}
