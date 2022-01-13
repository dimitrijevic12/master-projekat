using CSharpFunctionalExtensions;
using IssuerBank.Core.Interface.Repository;
using IssuerBank.Core.Interface.Service;
using IssuerBank.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IssuerBank.Core.Services
{
    public class MerchantService : IMerchantService
    {
        private readonly IMerchantRepository _merchantRepository;

        public MerchantService(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        public Result<Merchant> Create(string name)
        {
            if (String.IsNullOrWhiteSpace(name) || name.Length > 50)
                return Result.Failure<Merchant>("Name cannot be empty or have more than 50 characters");
            Guid id = Guid.NewGuid();
            Merchant merchant = _merchantRepository.GetById(id);
            while (merchant != null)
            {
                id = Guid.NewGuid();
                merchant = _merchantRepository.GetById(id);
            }
            Guid merchantId = Guid.NewGuid();
            merchant = _merchantRepository.GetByMerchantId(merchantId);
            while (merchant != null)
            {
                merchantId = Guid.NewGuid();
                merchant = _merchantRepository.GetByMerchantId(merchantId);
            }
            string merchantPassword = GeneratePassword(8);
            return Result.Success(_merchantRepository.Save(new Merchant(id, merchantId, merchantPassword, name)));
        }

        private string GeneratePassword(int length)
        {
            using (RNGCryptoServiceProvider cryptRNG = new RNGCryptoServiceProvider())
            {
                byte[] tokenBuffer = new byte[length];
                cryptRNG.GetBytes(tokenBuffer);
                return Convert.ToBase64String(tokenBuffer);
            }
        }
    }
}
