using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardPayment.Core.DTOs
{
    public class TransactionsPaymentIdDTO
    {
        public Guid OrderId { get; set; }
        public Guid PaymentId { get; set; }
    }
}
