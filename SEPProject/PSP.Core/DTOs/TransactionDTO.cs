using PSP.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Core.DTOs
{
    public class TransactionDTO
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid OrderId { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public Guid MerchantId { get; set; }
        public string MerchantName { get; set; }
        public Guid IssuerId { get; set; }
        public string IssuerName { get; set; }
        public string Type { get; set; }
    }
}
