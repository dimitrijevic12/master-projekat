using CSharpFunctionalExtensions;
using IssuerBank.Core.Interface.Repository;
using IssuerBank.Core.Interface.Service;
using IssuerBank.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuerBank.Core.Services
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
            if (!merchant.MerchantPassword.Equals(request.MerchantPassword))
                return Result.Failure<PSPRequest>("Incorrect merchant password.");
            if (request.Amount < 0)
                return Result.Failure<PSPRequest>("Amount can not be negative number.");
            return Result.Success(_PSPRequestRepository.Save(request));
        }
    }
}
