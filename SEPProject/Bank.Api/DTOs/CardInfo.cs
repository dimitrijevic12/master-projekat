using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Api.DTOs
{
    public class CardInfo
    {
        public Guid PaymentId { get; set; }
        public string PAN { get; set; }
        public string SecurityCode { get; set; }
        public string CardHolderName { get; set; }
        public string ExpirationDate { get; set; }
        public double Amount { get; set; }
        public string AcquirerAccountNumber { get; set; }
        public string AcquirerName { get; set; }
    }
}
