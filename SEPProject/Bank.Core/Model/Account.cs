using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Model
{
    public class Account
    {
        private Guid Id { get; set; }
        private string AccountNumber { get; set;  }
        private double Balance { get; set; }
        public virtual User User { get; set; }

        public Account(Guid id, string accountNumber, double balance, User user)
        {
            Id = id;
            AccountNumber = accountNumber;
            Balance = balance;
            User = user;
        }
    }
}
