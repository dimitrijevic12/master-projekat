using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace IssuerBank.Core.Model
{
    public class RegisteredUser : User
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string EmailAddress { get; private set; }
        public string UniquePersonalRegistrationNumber { get; private set; }
        public virtual ICollection<PaymentCard> PaymentCards { get; private set; }

        public RegisteredUser() : base()
        {
        }

        public RegisteredUser(Guid id, string firstName, string lastName, DateTime dateOfBirth, string emailAddress, string uniquePersonalRegistrationNumber)
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            EmailAddress = emailAddress;
            UniquePersonalRegistrationNumber = uniquePersonalRegistrationNumber;
        }
    }
}
