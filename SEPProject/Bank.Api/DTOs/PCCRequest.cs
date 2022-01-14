﻿using System;

namespace Bank.Api.DTOs
{
    public class PCCRequest
    {
        public Guid AcquirerOrderId { get; set; }
        public DateTime AcquirerTimestamp { get; set; }
        public Guid PaymentId { get; set; }
        public string PAN { get; set; }
        public string AcquirerBankPAN { get; set; }
        public string SecurityCode { get; set; }
        public string CardHolderName { get; set; }
        public string ExpirationDate { get; set; }
        public double Amount { get; set; }
        public string AcquirerAccountNumber { get; set; }
        public string AcquirerName { get; set; }
        public string Currency { get; set; }

        public PCCRequest(Guid acquirerOrderId, DateTime acquirerTimestamp, Guid paymentId, string pAN, string acquirerBankPAN,
            string securityCode, string cardHolderName, string expirationDate, double amount, string acquirerAccountNumber, string acquirerName, string currency)
        {
            AcquirerOrderId = acquirerOrderId;
            AcquirerTimestamp = acquirerTimestamp;
            PaymentId = paymentId;
            PAN = pAN;
            AcquirerBankPAN = acquirerBankPAN;
            SecurityCode = securityCode;
            CardHolderName = cardHolderName;
            ExpirationDate = expirationDate;
            Amount = amount;
            AcquirerAccountNumber = acquirerAccountNumber;
            AcquirerName = acquirerName;
            Currency = currency;
        }
    }
}