using CardPayment.Core.Interface.Repository;
using CardPayment.Core.Model;
using CardPayment.DataAccess.CardPaymentDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardPayment.DataAccess.Implementation
{
    public class MerchantRepository : Repository<Merchant>, IMerchantRepository
    {
        private AppDbContext dbContext;

        public MerchantRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }
    }
}
