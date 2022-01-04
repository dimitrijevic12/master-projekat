﻿using Bank.Core.Model;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Interface.Service
{
    public interface ITransactionService
    {
        public Result<Transaction> Create(double amount, string currency, DateTime timestamp, Guid paymentId, string pan, TransactionStatus transactionStatus);
    }
}
