using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Core.DTOs;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;
using WebShop.Core.Services;

namespace WebShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UPPController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUPPAccessRepository _uppAccessRepository;
        private readonly TransactionService transactionService;

        public UPPController(ITransactionRepository transactionRepository, IUPPAccessRepository uppAccessRepository,
        TransactionService transactionService)
        {
            _transactionRepository = transactionRepository;
            this.transactionService = transactionService;
            _uppAccessRepository = uppAccessRepository;
        }

        [HttpPost]
        public IActionResult Save(UPPItemTransaction uppItemTransaction)
        {
            transactionService.SaveUPPItemTransaction(uppItemTransaction);
            return Ok(uppItemTransaction);
        }

        [HttpPost("access")]
        public IActionResult SaveUPPAccess(UPPAccessRequestDTO uppAccess)
        {
            _uppAccessRepository.Save(new UPPAccess(new Guid(), uppAccess.accessPlace, DateTime.Parse(uppAccess.accessTimestamp)));
            return Ok(uppAccess);
        }

        [HttpGet]
        public UPPAccessDTO GetUPPAccess()
        {
            IEnumerable<UPPAccess> uppAccessList =  _uppAccessRepository.GetAll();

            List<DateTime> dates = new List<DateTime>();
            foreach(UPPAccess uppAccess in uppAccessList)
            {
                dates.Add(uppAccess.Timestamp);
            }

            List<string> places = new List<string>();
            foreach (UPPAccess uppAccess in uppAccessList)
            {
                places.Add(uppAccess.Place);
            }

            DateTime highCountValue = DateTime.Now;
            int count = int.MinValue;
            dates.ForEach(dateTime =>
            {
                var potentialCount = dates.Count(d => d == dateTime);
                if (potentialCount > count)
                {
                    count = potentialCount;
                    highCountValue = dateTime;
                }
            });

            string highCountValuePlace = "Novi Sad";
            int countPlaces = int.MinValue;
            places.ForEach(place =>
            {
                var potentialCount = places.Count(d => d.Equals(place));
                if (potentialCount > countPlaces)
                {
                    countPlaces = potentialCount;
                    highCountValuePlace = place;
                }
            });

            string hour = highCountValue.Hour.ToString(); 

            return new UPPAccessDTO(highCountValuePlace, hour);
        }
    }

        
}
