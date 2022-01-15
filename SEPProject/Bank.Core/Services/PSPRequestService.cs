using Bank.Core.Interface.Repository;
using Bank.Core.Interface.Service;
using Bank.Core.Model;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace Bank.Core.Services
{
    public class PSPRequestService : IPSPRequestService
    {
        private readonly IPSPRequestRepository _PSPRequestRepository;
        private readonly IMerchantRepository _merchantRepository;

        public PSPRequestService(IPSPRequestRepository pSPRequestRepository, IMerchantRepository merchantRepository)
        {
            _PSPRequestRepository = pSPRequestRepository;
            _merchantRepository = merchantRepository;
        }

        public Result<PSPRequest> Create(PSPRequest request)
        {
            if (_PSPRequestRepository.GetById(request.Id) != null)
                return Result.Failure<PSPRequest>("PSP request with that Id already exists.");
            Merchant merchant = _merchantRepository.GetByMerchantId(request.MerchantId);
            if (merchant == null)
                return Result.Failure<PSPRequest>("Merchant with that Id does not exists.");
            if (!merchant.MerchantPassword.Equals(GetHashCode(request.MerchantPassword, merchant.Salt)))
                return Result.Failure<PSPRequest>("Incorrect merchant password.");
            if (request.Amount < 0)
                return Result.Failure<PSPRequest>("Amount can not be negative number.");
            return Result.Success(_PSPRequestRepository.Save(request));
        }

        private static string GetHashCode(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}