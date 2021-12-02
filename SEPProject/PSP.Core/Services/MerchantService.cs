using PSP.Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Core.Services
{
    public class MerchantService
    {
        private readonly IMerchantRepository _merchantRepository;

        public MerchantService(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }
    }
}
