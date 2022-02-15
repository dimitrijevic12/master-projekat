using System;
using System.Collections.Generic;
using WebShop.Core.Model;

namespace WebShop.Core.Interface.Repository
{
    public interface IConferenceRepository : IRepository<Conference>
    {
        public IEnumerable<Conference> GetConferencesForOwner(Guid ownerId);

        public IEnumerable<Conference> GetFutureConferences();

        public Conference GetByName(string name);
    }
}