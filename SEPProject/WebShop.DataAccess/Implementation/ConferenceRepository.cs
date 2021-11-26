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
    }
}
