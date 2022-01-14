using IssuerBank.Core.Interface.Repository;
using IssuerBank.Core.Model;
using IssuerBank.DataAccess.BankDbContext;
using System;
using System.Linq;

namespace IssuerBank.DataAccess.Implementation
{
    public class MerchantRepository : Repository<Merchant>, IMerchantRepository
    {
        private AppDbContext dbContext;

        public MerchantRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }

        public Merchant GetByMerchantId(Guid merchantId)
        {
            return dbContext.Merchants.ToList().Where(merchant => merchant.MerchantId == merchantId).FirstOrDefault();
        }
    }
}