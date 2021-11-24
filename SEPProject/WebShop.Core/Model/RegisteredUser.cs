using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.Model
{
    public class RegisteredUser : User
    {
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private string PostalCode { get; set; }
        private string PhoneNumber { get; set; }
        private string Address { get; set; }

        public RegisteredUser(Guid id, MailAddress email, string username, string password,
            string firstName, string lastName, string postalCode, string phoneNumber, string address)
            : base(id, email, username, password)
        {
            FirstName = firstName;
            LastName = lastName;
            PostalCode = postalCode;
            PhoneNumber = phoneNumber;
            Address = address;
        }
    }
}
