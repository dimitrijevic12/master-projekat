using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;
using WebShop.DataAccess.WebShopDbContext;

namespace WebShop.DataAccess.Implementation
{
    public class UPPAccessRepository : Repository<UPPAccess>, IUPPAccessRepository
    {
        private AppDbContext dbContext;

        public UPPAccessRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }
    }
}
