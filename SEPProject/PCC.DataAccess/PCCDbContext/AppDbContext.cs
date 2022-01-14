using Microsoft.EntityFrameworkCore;
using PCC.Core.Model;
using System;

namespace PCC.DataAccess.PCCDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bank>().HasData(
                new Bank(new Guid("12345678-1234-1234-1234-123412341234"), "Acquirer bank", "123456", new Uri("https://localhost:44375/api/")),
                new Bank(new Guid("12345678-1234-1234-1234-222222222222"), "Issuer bank", "222222", new Uri("https://localhost:44376/api/"))
                );

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction(new Guid("12345678-1234-1234-1234-123412341234"), 444.0, "EUR", DateTime.Now,
                                new Guid("12345678-1234-1234-1234-123412341234"), TransactionStatus.Success,
                                new Guid("12345678-1234-1234-1234-123412341235"), DateTime.Now,
                                new Guid("12345678-1234-1234-1234-123412341235"), "Acquirer Bank name",
                                new Guid("12345678-1234-1234-1234-123412341234"), "Issuer Bank name",
                                new Guid("12345678-1234-1234-1234-123412341234"), DateTime.Now)
             );
        }
    }
}