using System;
using System.Collections.Generic;
using System.Linq;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;
using WebShop.DataAccess.WebShopDbContext;

namespace WebShop.DataAccess.Implementation
{
    public class ConferenceRepository : Repository<Conference>, IConferenceRepository
    {
        private AppDbContext dbContext;

        public ConferenceRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }

        public IEnumerable<Conference> GetConferencesForOwner(Guid ownerId)
        {
            return dbContext.Conferences.ToList().Where(conference => conference.OwnerId == ownerId).ToList();
        }
    }
}
