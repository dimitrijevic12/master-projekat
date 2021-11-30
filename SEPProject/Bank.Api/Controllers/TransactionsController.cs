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
    public class TransactionsController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPaymentCardService _paymentCardService;
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionRepository transactionRepository, IPaymentCardService paymentCardService,
            ITransactionService transactionService)
        {
            _transactionRepository = transactionRepository;
            _paymentCardService = paymentCardService;
            _transactionService = transactionService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_transactionRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Create(CardInfo cardInfo)
        {
            if(_transactionRepository.GetByPaymentId(cardInfo.PaymentId) != null)
                return BadRequest("Transaction with given payment id already exists.");
            // TODO
            /*if (!cardInfo.PAN.Substring(0, 5).Equals("123456"))
            {
                send to pcc
            }*/
            Result result = _paymentCardService.Pay(new Core.Model.PaymentCard(Guid.Empty, cardInfo.PAN, cardInfo.SecurityCode, cardInfo.CardHolderName,
                cardInfo.ExpirationDate, Guid.Empty), cardInfo.Amount);
            if (result.IsFailure)
                return BadRequest(result.Error);
            Result<Transaction> transactionResult = _transactionService.Create(cardInfo.Amount, DateTime.Now, cardInfo.PaymentId, cardInfo.PAN);
            return Ok(transactionResult.Value);
        }
    }
}
