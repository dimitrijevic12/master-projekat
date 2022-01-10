using System;

namespace PCC.Api.DTOs
{
    public class PCCResponse
    {
        public string TransactionStatus { get; set; }
        public Guid AcquirerOrderId { get; set; }
        public DateTime AcquirerTimestamp { get; set; }
        public Guid IssuerOrderId { get; set; }
        public DateTime IssuerTimestamp { get; set; }

        public PCCResponse(string transactionStatus, Guid acquirerOrderId, DateTime acquirerTimestamp, Guid issuerOrderId, DateTime issuerTimestamp)
        {
            TransactionStatus = transactionStatus;
            AcquirerOrderId = acquirerOrderId;
            AcquirerTimestamp = acquirerTimestamp;
            IssuerOrderId = issuerOrderId;
            IssuerTimestamp = issuerTimestamp;
        }
    }
}