using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.DTOs
{
    public class UPPAccessDTO
    {
        public string place { get; set; }
        public string timestamp { get; set; }
        public UPPAccessDTO(string place, string timestamp)
        {
            this.place = place;
            this.timestamp = timestamp;
        }
    }
}
