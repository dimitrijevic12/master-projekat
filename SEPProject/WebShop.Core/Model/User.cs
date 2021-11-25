using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Model
{
    public abstract class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        protected User()
        {
        }

        protected User(Guid id, string email, string username, string password)
        {
            Id = id;
            Email = email;
            Username = username;
            Password = password;
        }

        
    }
}
