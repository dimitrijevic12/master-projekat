using System;
using System.Collections.Generic;
using System.Linq;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;
using WebShop.DataAccess.WebShopDbContext;

namespace WebShop.DataAccess.Implementation
{
    public class TransportationRepository : Repository<Transportation>, ITransportationRepository
    {
        private AppDbContext dbContext;

        public TransportationRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }

        public IEnumerable<Transportation> GetTransportationsForDestinations(string startDestination, string finalDestination)
        {
            if (!String.IsNullOrEmpty(startDestination))
            {
                return GetForStartDestinations(startDestination, finalDestination);
            }
            else if (!String.IsNullOrEmpty(finalDestination))
            {
                return GetOnlyForFinalDestination(finalDestination);
            }
            else
            {
                return dbContext.Transportations.ToList().Where(transportation => transportation.DepartureTime > DateTime.Now).ToList();
            }
        }

        private IEnumerable<Transportation> GetOnlyForFinalDestination(string finalDestination)
        {
            return dbContext.Transportations.ToList().Where(transportation =>
                                transportation.FinalDestination.Equals(finalDestination) &&
                                transportation.DepartureTime > DateTime.Now).ToList();
        }

        private IEnumerable<Transportation> GetForStartDestinations(string startDestination, string finalDestination)
        {
            return !String.IsNullOrEmpty(finalDestination)
                ? GetForBothDestinations(startDestination, finalDestination)
                : GetOnlyForStartDestination(startDestination);
        }

        private IEnumerable<Transportation> GetOnlyForStartDestination(string startDestination)
        {
            return dbContext.Transportations.ToList().Where(transportation =>
                            transportation.StartDestination.Equals(startDestination) &&
                            transportation.DepartureTime > DateTime.Now).ToList();
        }

        private IEnumerable<Transportation> GetForBothDestinations(string startDestination, string finalDestination)
        {
            return dbContext.Transportations.ToList().Where(transportation =>
                            transportation.StartDestination.Equals(startDestination) &&
                            transportation.FinalDestination.Equals(finalDestination) &&
                            transportation.DepartureTime > DateTime.Now).ToList();
        }

        public IEnumerable<Transportation> GetTransportationsForOwner(Guid ownerId)
        {
            return dbContext.Transportations.ToList().Where(transportation => transportation.OwnerId == ownerId).ToList();
        }

        public Transportation GetTransportationByName(string name)
        {
            return dbContext.Transportations.FirstOrDefault(transportation => transportation.Name.Equals(name));
        }
    }
}