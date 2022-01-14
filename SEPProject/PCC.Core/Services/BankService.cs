using PCC.Core.Interfaces.Repository;

namespace PCC.Core.Services
{
    public class BankService
    {
        private readonly IBankRepository _bankRepository;

        public BankService(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }
    }
}