using System;

namespace WebShop.Core.Model
{
    public class Admin : User
    {
        public string Name { get; set; }

        public Admin() : base ()
        {
        }

        public Admin(Guid id, string email, string username, string password, string name) : base(id, email, username, password)
        {
            Name = name;
        }
    }
}
