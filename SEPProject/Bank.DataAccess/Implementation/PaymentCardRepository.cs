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
    public class PaymentCardRepository : Repository<PaymentCard>, IPaymentCardRepository
    {
        private AppDbContext dbContext;

        public PaymentCardRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }
    }
}
