using CSharpFunctionalExtensions;
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
        public double ReservedBalance { get; private set; }
        public Guid UserId { get; private set; }
        public virtual User User { get; private set; }

        public Account()
        {
        }

        public Account(Guid id, string accountNumber, double balance, double reservedBalance, Guid userId)
        {
            Id = id;
            AccountNumber = accountNumber;
            Balance = balance;
            ReservedBalance = reservedBalance;
            UserId = userId;
        }

        public Result<double> ReserveBalance(double amount)
        {
            if (amount < 0)
                return Result.Failure<double>("Amount can not be negative number");
            if(Balance - amount < 0)
                return Result.Failure<double>("There is not enough resources on this bank account.");
            Balance = Balance - amount;
            return Result.Success(amount);
        }
    }
}
