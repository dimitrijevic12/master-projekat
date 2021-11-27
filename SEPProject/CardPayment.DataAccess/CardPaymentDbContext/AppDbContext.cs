using CardPayment.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardPayment.DataAccess.CardPaymentDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Merchant> Merchants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {         
            modelBuilder.Entity<Merchant>().HasData(
                new Merchant(new Guid("12345678-1234-1234-1234-123412341234"), new Guid("12345678-1234-1234-1234-123412341232"), "password", "Name")
             );
        }
    }
}
