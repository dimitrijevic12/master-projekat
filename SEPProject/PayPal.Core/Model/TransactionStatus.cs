using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPal.Core.Model
{
    public enum TransactionStatus
    {
        Pending,
        Success,
        Failed,
        Error
    }
}
