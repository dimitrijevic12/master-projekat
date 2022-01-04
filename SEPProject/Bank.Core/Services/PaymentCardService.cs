using Bank.Core.Interface.Repository;
using Bank.Core.Interface.Service;
using Bank.Core.Model;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Services
{
    public class PaymentCardService : IPaymentCardService
    {
        private readonly IPaymentCardRepository _paymentCardRepository;
        private readonly IAccountRepository _accountRepository;

        public PaymentCardService(IPaymentCardRepository paymentCardRepository, IAccountRepository accountRepository)
        {
            _paymentCardRepository = paymentCardRepository;
            _accountRepository = accountRepository;
        }

        public Result Pay(PaymentCard paymentCard, double amount, string currency, string acquirerAccountNumber)
        {
            PaymentCard card = _paymentCardRepository.GetByPAN(paymentCard.PAN);
            if (card == null)
                return Result.Failure("Payment card with given PAN does not exist.");
            if(!paymentCard.ExpirationDate.Equals(card.ExpirationDate))
                return Result.Failure("Invalid expiration date.");
            if (!paymentCard.SecurityCode.Equals(card.SecurityCode))
                return Result.Failure("Invalid security code.");
            if (!paymentCard.HolderName.Equals(card.HolderName))
                return Result.Failure("Invalid card holder name.");
            Account issuerAccount = _accountRepository.GetByUserId(card.CardOwnerId);
            amount = GetAmountBasedOnCurrency(amount, currency);
            Result reserveBalanceResult = issuerAccount.ReserveBalance(amount);
            if (reserveBalanceResult.IsFailure)
                return (reserveBalanceResult);
            Account acquirerAccount = _accountRepository.GetByAccountNumber(acquirerAccountNumber);
            Result increaseBalanceResult = acquirerAccount.IncreaseBalance(amount);
            if (increaseBalanceResult.IsFailure)
                return (increaseBalanceResult);
            _accountRepository.Edit(issuerAccount);
            _accountRepository.Edit(acquirerAccount);
            return Result.Combine(reserveBalanceResult, increaseBalanceResult);
        }

        private static double GetAmountBasedOnCurrency(double amount, string currency)
        {
            return currency switch
            {
                "EUR" => amount,
                "USD" => amount / 1.13,
                "RSD" => amount / 117.57,
                _ => amount,
            };
        }
    }
}
