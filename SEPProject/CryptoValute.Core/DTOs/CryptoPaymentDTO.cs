using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoValute.Core.DTOs
{
    public class CryptoPaymentDTO
    {
        public string payment_url { get; set; }

        public CryptoPaymentDTO(string payment_url)
        {
            this.payment_url = payment_url;
        }
    }
}
