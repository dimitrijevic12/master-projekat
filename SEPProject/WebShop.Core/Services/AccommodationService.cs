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
    }
}
