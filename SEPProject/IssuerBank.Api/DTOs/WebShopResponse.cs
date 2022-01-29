using System;

namespace IssuerBank.Api.DTOs
{
    public class WebShopResponse
    {
        public Guid TransactionId { get; set; }

        public string PerdiemStatus { get; set; }

        public WebShopResponse(Guid transactionId, string perdiemStatus)
        {
            TransactionId = transactionId;
            PerdiemStatus = perdiemStatus;
        }
    }
}