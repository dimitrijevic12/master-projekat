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
