using CSharpFunctionalExtensions;
using PSP.Core.DTOs;
using PSP.Core.Interface.Repository;
using PSP.Core.Model;
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
        private readonly IRegisteredWebShopRepository _registeredWebShopRepository;

        public MerchantService(IMerchantRepository merchantRepository, IRegisteredWebShopRepository registeredWebShopRepository)
        {
            _merchantRepository = merchantRepository;
            _registeredWebShopRepository = registeredWebShopRepository;
        }

        public Result Save(MerchantDTO merchantDTO)
        {
            if (_merchantRepository.GetByMerchantId(merchantDTO.MerchantId) != null) return Result.Failure("Merchant with that MerchantId already exists!");
            if (merchantDTO.MerchantPassword == "") return Result.Failure("Password can't be empty!");
            if (_registeredWebShopRepository.GetById(merchantDTO.RegisteredWebShopId) == null ) return Result.Failure("There is no WebShop with that Id!");
            Merchant merchant = _merchantRepository.Save(new Merchant(merchantDTO.Id, merchantDTO.MerchantId, merchantDTO.MerchantPassword, merchantDTO.Name, merchantDTO.RegisteredWebShopId));
            return Result.Success(merchant);
        }

    }
}
