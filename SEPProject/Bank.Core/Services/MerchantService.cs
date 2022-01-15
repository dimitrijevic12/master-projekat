using Bank.Core.Interface.Repository;
using Bank.Core.Interface.Service;
using Bank.Core.Model;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace Bank.Core.Services
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
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            string saltString = Convert.ToBase64String(salt);
            Merchant savedMerchant = new Merchant(id, merchantId, GetHashCode(merchantPassword, salt), name, saltString);
            _merchantRepository.Save(savedMerchant);
            Merchant merchantForWebStore = new Merchant(id, merchantId, merchantPassword, name, saltString);
            return Result.Success(merchantForWebStore);
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

        private static string GetHashCode(string password, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}