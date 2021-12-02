using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Core.DTOs
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String WebShopName { get; set; }
    }
}
