using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Core.DTOs
{
    public class MerchantDTO
    {
        public Guid Id { get; set; }
        public Guid MerchantId { get; set; }
        public string MerchantPassword { get; set; }
        public string Name { get; set; }
        public Guid RegisteredWebShopId { get; set; }
    }
}
