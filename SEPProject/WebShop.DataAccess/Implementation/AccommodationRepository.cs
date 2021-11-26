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
    }
}
