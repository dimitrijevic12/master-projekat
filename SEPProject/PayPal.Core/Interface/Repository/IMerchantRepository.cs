using PayPal.Core.Model;
using System;

namespace PayPal.Core.Interface.Repository
{
    public interface IMerchantRepository : IRepository<Merchant>
    {
        public Merchant GetByMerchantId(Guid id);
    }
}
