using Bank.Core.Interface.Repository;
using Bank.Core.Model;
using Bank.DataAccess.BankDbContext;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DataAccess.Implementation
{
    public class PSPResponseRepository : Repository<PSPResponse>, IPSPResponseRepository
    {
        private AppDbContext dbContext;

        public PSPResponseRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }

        public PSPResponse GetByPaymentId(Guid id) => dbContext.PSPResponse.ToList()
            .Where(response => response.PaymentId == id).FirstOrDefault();
    }
}
