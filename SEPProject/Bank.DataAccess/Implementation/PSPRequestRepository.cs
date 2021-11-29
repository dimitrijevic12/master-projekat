using Bank.Core.Interface.Repository;
using Bank.Core.Model;
using Bank.DataAccess.BankDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DataAccess.Implementation
{
    public class PSPRequestRepository : Repository<PSPRequest>, IPSPRequestRepository
    {
        private AppDbContext dbContext;

        public PSPRequestRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }
    }
}
