using Bank.Api.DTOs;
using Bank.Core.Interface.Repository;
using Bank.Core.Interface.Service;
using Bank.Core.Model;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PSPRequestsController : Controller
    {
        private readonly IPSPRequestRepository _PSPRequestRepository;
        private readonly IPSPRequestService _PSPRequestService;
        private readonly IPSPResponseService _PSPResponseService;
        private readonly IPSPResponseRepository _PSPResponseRepository;

        public PSPRequestsController(IPSPRequestRepository pSPRequestRepository, IPSPRequestService pSPRequestService,
            IPSPResponseService pSPResponseService, IPSPResponseRepository pSPResponseRepository)
        {
            _PSPRequestRepository = pSPRequestRepository;
            _PSPRequestService = pSPRequestService;
            _PSPResponseService = pSPResponseService;
            _PSPResponseRepository = pSPResponseRepository;
        }

        [HttpPost]
        public IActionResult Create(DTOs.PSPRequest pspRequest)
        {
            Guid id = Guid.NewGuid();
            Core.Model.PSPRequest request = new Core.Model.PSPRequest(id, pspRequest.MerchantId, pspRequest.MerchantPassword, pspRequest.Amount,
                pspRequest.MerchantOrderId, pspRequest.MerchantTimestamp, pspRequest.SuccessUrl, pspRequest.FailedUrl, pspRequest.ErrorUrl);
            Result<Core.Model.PSPRequest> result = _PSPRequestService.Create(request);
            if (result.IsFailure)
                return BadRequest(result.Error);
            Guid paymentId = Guid.NewGuid();
            Result<Core.Model.PSPResponse> response = _PSPResponseService.Create(request.Id);
            if(response.IsFailure)
                return BadRequest(result.Error);
            return Created(this.Request.Path + "/" + result.Value.Id, new BankResponse(response.Value.PaymentUrl, response.Value.PaymentId));
        }

        [HttpGet]
        public IActionResult Find([FromQuery] Guid paymentId)
        {
            var test = _PSPResponseRepository.GetByPaymentId(paymentId);
            return Ok(_PSPResponseRepository.GetByPaymentId(paymentId).PSPRequest);
        }
    }
}
