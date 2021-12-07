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
    public class AccommodationService
    {
        private readonly IAccommodationRepository _accommodationRepository;

        public AccommodationService(IAccommodationRepository accommodationRepository)
        {
            _accommodationRepository = accommodationRepository;
        }

        public IEnumerable<Accommodation> GetAccommodations(string city)
        {
            if (String.IsNullOrEmpty(city))
            {
               return _accommodationRepository.GetAll();
            }
            return _accommodationRepository.GetAccommodationsForCity(city);
        }

        public Result Save(Accommodation accommodation)
        {
            if (String.IsNullOrEmpty(accommodation.Name) || String.IsNullOrEmpty(accommodation.CostPerNight.ToString()) ||
                String.IsNullOrEmpty(accommodation.ImagePath))
            {
                return Result.Failure("Name, price or image can't be empty!");
            }
            if (String.IsNullOrEmpty(accommodation.City))
            {
                return Result.Failure("City can't be empty!");
            }
            if (accommodation.CostPerNight < 0)
            {
                return Result.Failure("Invalid price!");
            }
            _accommodationRepository.Save(accommodation);
            return Result.Success(accommodation);
        }
    }
}
