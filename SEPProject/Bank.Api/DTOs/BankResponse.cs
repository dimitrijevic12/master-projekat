using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Api.DTOs
{
    public class BankResponse
    {
        public Uri PaymentUrl { get; set; }
        public Guid PaymentId { get; set; }

        public BankResponse(Uri paymentUrl, Guid paymentId)
        {
            PaymentUrl = paymentUrl;
            PaymentId = paymentId;
        }
    }
}
