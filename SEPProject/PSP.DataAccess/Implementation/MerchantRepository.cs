using PSP.Core.Interface.Repository;
using PSP.Core.Model;
using PSP.DataAccess.PSPDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.DataAccess.Implementation
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
            return dbContext.Merchants.ToList().FirstOrDefault(merchant => merchant.MerchantId.Equals(id));
        }
    }
}
