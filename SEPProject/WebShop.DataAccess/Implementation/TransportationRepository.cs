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
    }
}
