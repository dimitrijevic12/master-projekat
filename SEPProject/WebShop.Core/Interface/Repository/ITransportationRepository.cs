using System;
using System.Collections.Generic;
using WebShop.Core.Model;

namespace WebShop.Core.Interface.Repository
{
    public interface ITransportationRepository : IRepository<Transportation>
    {
        public IEnumerable<Transportation> GetTransportationsForDestinations(string startDestination, string finalDestination);
        public IEnumerable<Transportation> GetTransportationsForOwner(Guid ownerId);
    }
}
