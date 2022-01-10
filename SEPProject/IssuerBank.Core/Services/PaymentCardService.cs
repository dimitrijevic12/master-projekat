using CSharpFunctionalExtensions;
using IssuerBank.Core.Interface.Repository;
using IssuerBank.Core.Interface.Service;
using IssuerBank.Core.Model;

namespace IssuerBank.Core.Services
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
            if (!paymentCard.ExpirationDate.Equals(card.ExpirationDate))
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
            _accountRepository.Edit(issuerAccount);
            return Result.Success(reserveBalanceResult);
        }

        private static double GetAmountBasedOnCurrency(double amount, string currency)
        {
            return currency switch
            {
                "EUR" => amount,
                "USD" => amount / 1.13,
                "RSD" => amount / 117.57,
                "CAD" => amount / 1.43,
                _ => amount,
            };
        }
    }
}