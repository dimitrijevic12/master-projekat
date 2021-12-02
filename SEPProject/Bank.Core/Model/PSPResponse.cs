using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Core.Model
{
    public class PSPResponse
    {
        public Guid Id { get; private set; }
        public Uri PaymentUrl { get; private set; }
        public Guid PaymentId { get; private set; }
        public Guid PSPRequestId { get; private set; }
        public virtual PSPRequest PSPRequest{get; private set; }

        public PSPResponse(Guid id, Uri paymentUrl, Guid paymentId, Guid pSPRequestId)
        {
            Id = id;
            PaymentUrl = paymentUrl;
            PaymentId = paymentId;
            PSPRequestId = pSPRequestId;
        }

        public PSPResponse()
        {
        }
    }
}
