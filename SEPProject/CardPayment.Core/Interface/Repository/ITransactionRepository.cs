using CardPayment.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardPayment.Core.Interface.Repository
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        public Transaction GetTransactionByOrderId(Guid orderId);
    }
}
