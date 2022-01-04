using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebShop.Core.Model;

namespace WebShop.DataAccess.WebShopDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionItem> TransactionItems { get; set; }
        public DbSet<Transportation> Transportations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RegisteredUser issuer = new RegisteredUser(Guid.NewGuid(), "issuer@gmail.com", "issuer", "password", "issuer", "issuer", "1234", "123456789", "Novosadska 14", "default");
            RegisteredUser headOfAcquirement = new RegisteredUser(Guid.NewGuid(), "headOfAcquirement@gmail.com", "headOfAcquirement", "password", "headOfAcquirement", "headOfAcquirement", "1234", "123456789", "Beogradska 26", "headOfAcquirement");
            RegisteredUser staff = new RegisteredUser(Guid.NewGuid(), "staff@gmail.com", "staff", "password", "staff", "staff", "1234", "123456789", "Novosadska 14", "staff");
            modelBuilder.Entity<RegisteredUser>().HasData(issuer, headOfAcquirement, staff);

            Admin admin = new Admin(new Guid("12345678-1234-1234-1234-123412341234"), "admin@gmail.com", "admin", "password", "Admin", new Guid("12345678-1234-1234-1234-123412341234"));
            modelBuilder.Entity<Admin>().HasData(admin);

            modelBuilder.Entity<Item>().HasData(
                new Item(new Guid("12345678-1234-1234-1234-123412341234"), "ItemName", "ItemDesc", 123.0, "bed.jpg", new Guid("12345678-1234-1234-1234-123412341234"))
            );

            modelBuilder.Entity<Accommodation>().HasData(
               new Accommodation(new Guid("12345678-1234-1234-1234-123412341234"), "AccomodationName", "AccomodationDesc", 1000.0, "Beogradska 14", "Belgrade", "95024620.jpg", new Guid("12345678-1234-1234-1234-123412341234"))
            );

            modelBuilder.Entity<Conference>().HasData(
               new Conference(new Guid("12345678-1234-1234-1234-123412341234"), "ConferenceName", "ConferenceDesc", false, DateTime.Now, 1400.0, "Beogradska 14", "conference12345123.jpg", new Guid("12345678-1234-1234-1234-123412341234"))
            );

            modelBuilder.Entity<Course>().HasData(
               new Course(new Guid("12345678-1234-1234-1234-123412341234"), "CourseName", "CourseDesc", false, DateTime.Now, DateTime.Now, 1400.0, "Beogradska 14", "conferences-integrated-systems-events-1500x630-2.jpg", new Guid("12345678-1234-1234-1234-123412341234"))
            );

            modelBuilder.Entity<Transaction>().HasData(
               new Transaction(new Guid("12345678-1234-1234-1234-123412341234"), TransactionStatus.Pending, DateTime.Now, 1640.0, "EUR", admin.Id, issuer.Id)
            );

            TransactionItem transactionItem1 = new TransactionItem(new Guid("12345678-1234-1234-1234-123412341234"), TransactionItemType.Item, new Guid("12345678-1234-1234-1234-123412341234"), "ItemName", 4, 150.0, new Guid("12345678-1234-1234-1234-123412341234"));
            TransactionItem transactionItem2 = new TransactionItem(new Guid("12345678-1234-1234-1234-123412341235"), TransactionItemType.Course, new Guid("12345678-1234-1234-1234-123412341234"), "CourseName", 1, 1400.0, new Guid("12345678-1234-1234-1234-123412341234"));
            modelBuilder.Entity<TransactionItem>().HasData(
               transactionItem1, transactionItem2
            );


            modelBuilder.Entity<Transportation>().HasData(
               new Transportation(new Guid("12345678-1234-1234-1234-123412341234"), "TransportationName", "TransportationDesc", 1000.0, "Belgrade", "Novi Sad", DateTime.Now, "conferences-integrated-systems-events-1500x630-2.jpg", new Guid("12345678-1234-1234-1234-123412341234"))
            );

            /*modelBuilder
             .Entity<TransactionItem>()
             .HasOne(p => p.Transaction)
             .WithMany(c => c.TransactionItems)
             .HasForeignKey(p => p.TransactionId);*/
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }
        }
    }
}
