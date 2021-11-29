using PSP.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Core.DTOs
{
    public class RegisteredWebShopDTO
    {
        public Guid Id { get; set; }
        public int WebShopId { get; set; }
        public string WebShopName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public Uri SuccessUrl { get; set; }
        public Uri FailedUrl { get; set; }
        public Uri ErrorUrl { get; set; }
        public List<PaymentType> PaymentTypes { get; set; }
    }
}
