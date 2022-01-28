using System;

namespace WebShop.Core.Model
{
    public class RegisteredUser : User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ITRole { get; set; }
        public string AccountNumber { get; set; }

        public RegisteredUser() : base()
        {
        }

        public RegisteredUser(Guid id, string email, string username, string password, string salt,
            string firstName, string lastName, string postalCode, string phoneNumber, string address, string itrole, string accountNumber)
            : base(id, email, username, password, salt)
        {
            FirstName = firstName;
            LastName = lastName;
            PostalCode = postalCode;
            PhoneNumber = phoneNumber;
            Address = address;
            ITRole = itrole;
            AccountNumber = accountNumber;
        }
    }
}
