using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.DTOs
{
    public class AdminDTO
    {
        public Guid AdminId { get; set; }
        public Guid MerchantId { get; set; }
    }
}
