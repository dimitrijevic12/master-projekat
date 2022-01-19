using CSharpFunctionalExtensions;
using IssuerBank.Api.DTOs;
using IssuerBank.Core.Interface.Repository;
using IssuerBank.Core.Interface.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace IssuerBank.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PSPRequestsController : Controller
    {
        private readonly ILogger<MerchantsController> _logger;
        private readonly IPSPRequestService _PSPRequestService;
        private readonly IPSPResponseService _PSPResponseService;
        private readonly IPSPResponseRepository _PSPResponseRepository;

        public PSPRequestsController(ILogger<MerchantsController> logger, IPSPRequestService pSPRequestService, IPSPResponseService pSPResponseService,
            IPSPResponseRepository pSPResponseRepository)
        {
            _logger = logger;
            _PSPRequestService = pSPRequestService;
            _PSPResponseService = pSPResponseService;
            _PSPResponseRepository = pSPResponseRepository;
        }

        [HttpPost]
        public IActionResult Create(DTOs.PSPRequest pspRequest)
        {
            Guid id = Guid.NewGuid();
            Core.Model.PSPRequest request = new Core.Model.PSPRequest(id, pspRequest.MerchantId, pspRequest.MerchantPassword, pspRequest.Amount,
                pspRequest.Currency, pspRequest.MerchantOrderId, pspRequest.MerchantTimestamp, pspRequest.SuccessUrl, pspRequest.FailedUrl, pspRequest.ErrorUrl);
            Result<Core.Model.PSPRequest> result = _PSPRequestService.Create(request);
            if (result.IsFailure)
            {
                _logger.LogError("Failed to create PSP request, Error: {@Error}", result.Error);
                return BadRequest(result.Error);
            }
            Guid paymentId = Guid.NewGuid();
            Result<Core.Model.PSPResponse> response = _PSPResponseService.Create(request.Id);
            if (response.IsFailure)
            {
                _logger.LogError("Failed to create PSP response, Error: {@Error}", result.Error);
                return BadRequest(result.Error);
            }
            _logger.LogInformation("Created PSP request {@PSPRequest}", new
            {
                pspRequest.MerchantId,
                pspRequest.Amount,
                pspRequest.Currency,
                pspRequest.MerchantOrderId,
                pspRequest.MerchantTimestamp,
                pspRequest.SuccessUrl,
                pspRequest.FailedUrl,
                pspRequest.ErrorUrl
            });
            return Created(this.Request.Path + "/" + result.Value.Id, new BankResponse(response.Value.PaymentUrl, response.Value.PaymentId));
        }

        [HttpGet]
        public IActionResult Find([FromQuery] Guid paymentId)
        {
            var pspResponse = _PSPResponseRepository.GetByPaymentId(paymentId);
            Core.Model.PSPRequest pspRequest = null;
            if (pspResponse == null)
            {
                _logger.LogInformation("Could not find PSP request with payment id: {@PaymentId}", paymentId);
                return Ok();
            }
            pspRequest = pspResponse.PSPRequest;
            _logger.LogInformation("Found PSP request {@PSPRequest} with payment id: {@PaymentId}", pspRequest, paymentId);
            return Ok(pspRequest);
        }
    }
}