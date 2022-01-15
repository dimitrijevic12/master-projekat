﻿using System;

namespace Bank.Core.Model
{
    public class PaymentCard
    {
        public Guid Id { get; private set; }
        public string PAN { get; private set; }
        public string SecurityCode { get; private set; }
        public string HolderName { get; private set; }
        public string ExpirationDate { get; private set; }
        public Guid CardOwnerId { get; private set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual RegisteredUser CardOwner { get; private set; }

        public string Salt { get; private set; }

        public PaymentCard() : base()
        {
        }

        public PaymentCard(Guid id, string pAN, string securityCode, string holderName, string expirationDate, Guid cardOwnerId, string salt)
        {
            Id = id;
            PAN = pAN;
            SecurityCode = securityCode;
            HolderName = holderName;
            ExpirationDate = expirationDate;
            CardOwnerId = cardOwnerId;
            Salt = salt;
        }
    }
}