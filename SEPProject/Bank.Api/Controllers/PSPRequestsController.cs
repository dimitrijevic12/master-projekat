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

        public PSPRequestsController(IPSPRequestRepository pSPRequestRepository, IPSPRequestService pSPRequestService,
            IPSPResponseService pSPResponseService)
        {
            _PSPRequestRepository = pSPRequestRepository;
            _PSPRequestService = pSPRequestService;
            _PSPResponseService = pSPResponseService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_PSPRequestRepository.GetAll());
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
            return Created(this.Request.Path + "/" + id, new BankResponse(new Uri("https://localhost:3001/" + paymentId), paymentId));
        }
    }
}
