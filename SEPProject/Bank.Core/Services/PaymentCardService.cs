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

        public Result Pay(PaymentCard paymentCard, double amount)
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
            Account account = _accountRepository.GetByUserId(card.CardOwnerId);
            Result reserveBalanceResult = account.ReserveBalance(amount);
            if (reserveBalanceResult.IsFailure)
                return (reserveBalanceResult);
            _accountRepository.Edit(account);
            return reserveBalanceResult;
        }
    }
}
