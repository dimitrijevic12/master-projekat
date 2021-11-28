using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;

namespace WebShop.Core.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionItemRepository _transactionItemRepository;

        public TransactionService(ITransactionRepository transactionRepository,
            ITransactionItemRepository transactionItemRepository)
        {
            _transactionRepository = transactionRepository;
            _transactionItemRepository = transactionItemRepository;
        }

        public void Save(Transaction transaction)
        {
            _transactionRepository.Save(transaction);
        }
    }
}
