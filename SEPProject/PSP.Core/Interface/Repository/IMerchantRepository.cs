using PSP.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Core.Interface.Repository
{
    public interface IMerchantRepository : IRepository<Merchant>
    {
        public Merchant GetByMerchantId(Guid id);
    }
}
