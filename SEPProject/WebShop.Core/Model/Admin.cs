using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Model
{
    public class Admin : User
    {
        private string Name { get; set; }

        public Admin(Guid id, MailAddress email, string username, string password, string name) : base(id, email, username, password)
        {
            Name = name;
        }
    }
}
