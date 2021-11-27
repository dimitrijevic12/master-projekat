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
    public class PaymentTypeRepository : Repository<PaymentType>, IPaymentTypeRepository
    {
        private AppDbContext dbContext;

        public PaymentTypeRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }
    }
}