using PCC.Core.Interfaces.Repository;
using PCC.Core.Model;
using PCC.DataAccess.PCCDbContext;
using System.Linq;

namespace PCC.DataAccess.Implementation
{
    public class BankRepository : Repository<Bank>, IBankRepository
    {
        private AppDbContext dbContext;

        public BankRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }

        public Bank GetByPAN(string pan)
        {
            return dbContext.Banks.ToList().FirstOrDefault(bank => bank.PAN.Equals(pan));
        }
    }
}