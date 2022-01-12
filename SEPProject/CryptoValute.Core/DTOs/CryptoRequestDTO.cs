using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoValute.Core.DTOs
{
    public class CryptoRequestDTO
    {
        public double price_amount { get; set; }
        public string price_currency { get; set; }
        public string receive_currency { get; set; }
        public string success_url { get; set; }
        public string cancel_url { get; set; }

    }
}
