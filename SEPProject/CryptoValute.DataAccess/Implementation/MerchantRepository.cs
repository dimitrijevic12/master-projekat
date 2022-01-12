using CryptoValute.Core.Interface.Repository;
using CryptoValute.Core.Model;
using CryptoValute.DataAccess.CryptoValuteDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoValute.DataAccess.Implementation
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
