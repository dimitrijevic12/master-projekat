using System;
using System.Collections.Generic;
using System.Linq;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;
using WebShop.DataAccess.WebShopDbContext;

namespace WebShop.DataAccess.Implementation
{
    public class AccommodationRepository : Repository<Accommodation>, IAccommodationRepository
    {
        private AppDbContext dbContext;

        public AccommodationRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }

        public IEnumerable<Accommodation> GetAccommodationsForCity(string city)
        {
            return dbContext.Accommodations.ToList().Where(accommodation => accommodation.City.Equals(city)).ToList();
        }

        public IEnumerable<Accommodation> GetAccommodationsForOwner(Guid ownerId)
        {
            return dbContext.Accommodations.ToList().Where(accommodation => accommodation.OwnerId == ownerId).ToList();
        }
    }
}
