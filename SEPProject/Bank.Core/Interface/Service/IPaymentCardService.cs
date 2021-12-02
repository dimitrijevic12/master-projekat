using Bank.Core.Model;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Interface.Service
{
    public interface IPaymentCardService
    {
        public Result Pay(PaymentCard paymentCard, double amount);
    }
}
