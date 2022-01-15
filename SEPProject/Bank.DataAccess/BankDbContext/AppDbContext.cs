using Bank.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;

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
        public DbSet<PaymentCard> PaymentCards { get; set; }
        public DbSet<PSPRequest> PSPRequests { get; set; }
        public DbSet<PSPResponse> PSPResponse { get; set; }
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Merchant merchant = new Merchant(new Guid("12345678-1234-1234-1234-123412341235"),
                new Guid("12345678-1234-1234-1234-123412341234"), "Vw73lwyE0LkxR3qGNGGefU2/9n6KmuyK68RHbcIlkBM=", "Merchant name",
                "DhR9MbXejS+TQxW3rvMT1g==");
            modelBuilder.Entity<Merchant>().HasData(
                merchant
             );

            RegisteredUser registeredUser = new RegisteredUser(new Guid("12345678-1234-1234-1234-123412341234"), "FirstName", "LastName", new DateTime(1990, 4, 4),
                "user@gmail.com", "123456789");
            RegisteredUser headOfAcquirement = new RegisteredUser(new Guid("d969bb55-393a-4b22-9507-f4b492b3413f"), "headOfAcquirement", "headOfAcquirement", new DateTime(1990, 4, 4),
                "headOfAcquirement@gmail.com", "123456790");
            RegisteredUser staff = new RegisteredUser(new Guid("665166bf-411c-4ba9-a16d-2a6460a59500"), "staff", "staff", new DateTime(1990, 4, 4),
               "staff@gmail.com", "123456791");
            modelBuilder.Entity<RegisteredUser>().HasData(
                registeredUser, headOfAcquirement, staff
             );

            modelBuilder.Entity<Account>().HasData(
                new Account(new Guid("12345678-1234-1234-1234-123412341234"), "123456789", 100000.0,
                            new Guid("12345678-1234-1234-1234-123412341235")),
                new Account(new Guid("12345678-1234-1234-1234-123412341235"), "222222222", 222222.0,
                            new Guid("12345678-1234-1234-1234-123412341234")),
                new Account(new Guid("12345678-1234-1234-1234-123412341236"), "333333333", 222222.0,
                            new Guid("d969bb55-393a-4b22-9507-f4b492b3413f")),
                new Account(new Guid("12345678-1234-1234-1234-123412341237"), "444444444", 222222.0,
                            new Guid("665166bf-411c-4ba9-a16d-2a6460a59500"))
             );

            modelBuilder.Entity<PaymentCard>().HasData(
                new PaymentCard(new Guid("12345678-1234-1234-1234-123412341234"), "1234561234561234", "d2sA8D1T2c/wKNU2Hjhq05XpX4wCcXack6TTUlZ3S+Q=",
                                "Holder Name", "04/22", new Guid("12345678-1234-1234-1234-123412341234"), "Al5Y4KoguBtLeK42dD/zgQ=="),
                new PaymentCard(new Guid("12345678-1234-1234-1234-123412341235"), "1234562222221234", "QNRiAvqu8ujlh7XkjCw9L0Vnb36QW8ypk7Yxr+4fVHs=",
                                "Acquirer Name", "04/22", new Guid("d969bb55-393a-4b22-9507-f4b492b3413f"), "DW7Xd4S1Ee6g8Nnhy2hNlw=="),
                new PaymentCard(new Guid("12345678-1234-1234-1234-123412341236"), "1234563333331234", "BAB0tLhEBlv0+Fb27ZIaLcHG9LxjU+MxFagRAKoi/Po=",
                                "Staff Name", "04/22", new Guid("665166bf-411c-4ba9-a16d-2a6460a59500"), "m85AArYCDZTgH1xZYNOL2w==")
             );

            modelBuilder.Entity<PSPRequest>().HasData(
                new PSPRequest(new Guid("12345678-1234-1234-1234-123412341234"),
                                new Guid("12345678-1234-1234-1234-123412341235"), "password", 123.0, "EUR",
                                new Guid("12345678-1234-1234-1234-123412341234"), DateTime.Now, new Uri("https://www.webshop.com/success"),
                                new Uri("https://www.webshop.com/failure"), new Uri("https://www.webshop.com/error"))
             );

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction(new Guid("12345678-1234-1234-1234-123412341234"), 444.0, "EUR", DateTime.Now,
                                new Guid("12345678-1234-1234-1234-123412341234"), TransactionStatus.Success,
                                new Guid("12345678-1234-1234-1234-123412341235"), "Acquirer name",
                                new Guid("12345678-1234-1234-1234-123412341234"), "Issuer name")
             );
        }
    }
}