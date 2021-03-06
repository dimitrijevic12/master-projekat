using Bank.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Interface.Repository
{
    public interface IPaymentCardRepository : IRepository<PaymentCard>
    {
        public PaymentCard GetByPAN(string pan);
    }
}
