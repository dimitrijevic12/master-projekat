using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Model
{
    public class Account
    {
        public Guid Id { get; private set; }
        public string AccountNumber { get; private set;  }
        public double Balance { get; private set; }
        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }

        public Account()
        {
        }

        public Account(Guid id, string accountNumber, double balance, Guid userId)
        {
            Id = id;
            AccountNumber = accountNumber;
            Balance = balance;
            UserId = userId;
        }
    }
}
