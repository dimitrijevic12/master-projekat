using PayPal.Core.Interface.Repository;
using PayPal.Core.Model;
using PayPal.DataAccess.PayPalDbContext;
using System;
using System.Linq;

namespace PayPal.DataAccess.Implementation
{
    public class MerchantRepository : Repository<Merchant>, IMerchantRepository
    {
        private AppDbContext dbContext;

        public MerchantRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }

        public Merchant GetByMerchantId(Guid id)
        {
            return dbContext.Merchants.ToList().FirstOrDefault(merchant => merchant.MerchantId == id);
        }
    }
}
