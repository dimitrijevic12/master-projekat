using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Model
{
    public class RegisteredUser : User
    {
        private string FirstName { get; set; }
        private string LastName { get; set; }
        private DateTime DateOfBirth { get; set; }
        private MailAddress EmailAddress { get; set; }
        private string UniquePersonalRegistrationNumber { get; set; }
        public virtual PaymentCard PaymentCard { get; set; }

        public RegisteredUser(Guid id, string firstName, string lastName, DateTime dateOfBirth, MailAddress emailAddress, string uniquePersonalRegistrationNumber,
            PaymentCard paymentCard) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            EmailAddress = emailAddress;
            UniquePersonalRegistrationNumber = uniquePersonalRegistrationNumber;
            PaymentCard = paymentCard;
        }
    }
}
