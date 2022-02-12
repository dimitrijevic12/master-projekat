using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Model
{
    public class UPPAccess
    {
        public Guid Id { get; set; }
        public string Place { get; set; }
        public DateTime Timestamp { get; set; }

        public UPPAccess()
        {
        }

        public UPPAccess(Guid id, string place, DateTime timestamp)
        {
            Id = id;
            Place = place;
            Timestamp = timestamp;
        }
    }
}
