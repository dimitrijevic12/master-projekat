using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Model
{
    public class PaymentCard
    {
        private Guid Id { get; set; }
        private string PAN { get; set; }
        private string SecurityCode { get; set; }
        private string HolderName { get; set; }
        private string ExpirationDate { get; set; }

        public PaymentCard(Guid id, string pAN, string securityCode, string holderName, string expirationDate)
        {
            Id = id;
            PAN = pAN;
            SecurityCode = securityCode;
            HolderName = holderName;
            ExpirationDate = expirationDate;
        }
    }
}
