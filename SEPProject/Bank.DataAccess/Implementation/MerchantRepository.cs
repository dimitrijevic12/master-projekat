using Bank.Core.Interface.Repository;
using Bank.Core.Model;
using Bank.DataAccess.BankDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DataAccess.Implementation
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
