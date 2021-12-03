using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;

namespace WebShop.Core.Services
{
    public class TransportationService
    {
        private readonly ITransportationRepository _transportationRepository;

        public TransportationService(ITransportationRepository transportationRepository)
        {
            _transportationRepository = transportationRepository;
        }

        public Result Save(Transportation transportation)
        {
            if (String.IsNullOrEmpty(transportation.Name) || String.IsNullOrEmpty(transportation.Price.ToString()))
            {
                return Result.Failure("Name or price can't be empty!");
            }
            if (String.IsNullOrEmpty(transportation.StartDestination) || String.IsNullOrEmpty(transportation.FinalDestination))
            {
                return Result.Failure("Start or final destination can't be empty!");
            }
            _transportationRepository.Save(transportation);
            return Result.Success(transportation);
        }
    }
}
