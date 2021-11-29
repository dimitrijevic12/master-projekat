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
    public class PaymentTypeRegisteredWebShopRepository : Repository<PaymentTypeRegisteredWebShop>, IPaymentTypeRegisteredWebShopRepository
    {
        private AppDbContext dbContext;

        public PaymentTypeRegisteredWebShopRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }
    }
}
