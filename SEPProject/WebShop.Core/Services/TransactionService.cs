using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void SaveTransactionItems(Transaction transaction)
        {
            foreach (TransactionItem transactionItem in transaction.TransactionItems)
            {
                transactionItem.Id = Guid.NewGuid();
                transactionItem.TransactionId = transaction.Id;
                _transactionItemRepository.Save(transactionItem);
            }
        }
    }
}
