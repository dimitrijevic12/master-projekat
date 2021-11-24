using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Model
{
    public abstract class User
    {
        protected Guid Id { get; set; }
        protected MailAddress Email { get; set; }
        protected string Username { get; set; }
        protected string Password { get; set; }

        protected User(Guid id, MailAddress email, string username, string password)
        {
            Id = id;
            Email = email;
            Username = username;
            Password = password;
        }
    }
}
