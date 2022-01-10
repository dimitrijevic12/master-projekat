using CSharpFunctionalExtensions;
using IssuerBank.Core.Interface.Repository;
using IssuerBank.Core.Model;
using IssuerBank.DataAccess.BankDbContext;
using System;
using System.Linq;

namespace IssuerBank.DataAccess.Implementation
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