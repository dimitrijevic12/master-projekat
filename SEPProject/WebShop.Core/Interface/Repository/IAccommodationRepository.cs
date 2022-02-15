using System;
using System.Collections.Generic;
using WebShop.Core.Model;

namespace WebShop.Core.Interface.Repository
{
    public interface IAccommodationRepository : IRepository<Accommodation>
    {
        public IEnumerable<Accommodation> GetAccommodationsForCity(string city);

        public IEnumerable<Accommodation> GetAccommodationsForOwner(Guid ownerId);

        public Accommodation GetByName(string name);
    }
}