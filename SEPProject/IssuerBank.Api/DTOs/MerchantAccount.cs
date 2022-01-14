using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssuerBank.Api.DTOs
{
    public class MerchantAccount
    {
        public string AcquirerAccountNumber { get; set; }
        public string AcquirerName { get; set; }

        public MerchantAccount(string acquirerAccountNumber, string acquirerName)
        {
            AcquirerAccountNumber = acquirerAccountNumber;
            AcquirerName = acquirerName;
        }
    }
}
