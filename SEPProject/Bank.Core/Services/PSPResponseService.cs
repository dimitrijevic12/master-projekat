using Bank.Core.Interface.Repository;
using Bank.Core.Interface.Service;
using Bank.Core.Model;
using CSharpFunctionalExtensions;
using System;

namespace Bank.Core.Services
{
    public class PSPResponseService : IPSPResponseService
    {
        private readonly IPSPResponseRepository _PSPResponseRepository;

        public PSPResponseService(IPSPResponseRepository pSPResponseRepository)
        {
            _PSPResponseRepository = pSPResponseRepository;
        }

        public Result<PSPResponse> Create(Guid pspRequestId)
        {
            Guid id = Guid.NewGuid();
            if (_PSPResponseRepository.GetById(id) != null)
                return Result.Failure<PSPResponse>("PSP response with that id already exists.");
            Guid paymentId = Guid.NewGuid();
            if (_PSPResponseRepository.GetByPaymentId(paymentId) != null)
                return Result.Failure<PSPResponse>("Payment Id already exists.");
            PSPResponse response = new PSPResponse(id, new Uri("https://172.20.10.3:3002/payment/" + paymentId), paymentId, pspRequestId);
            _PSPResponseRepository.Save(response);
            return Result.Success(response);
        }
    }
}