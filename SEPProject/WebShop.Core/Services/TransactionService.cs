using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using WebShop.Core.DTOs;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;

namespace WebShop.Core.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IRegisteredUserRepository _registeredUserRepository;
        private readonly IAdminRepository _adminRepository;

        public TransactionService(ITransactionRepository transactionRepository, IItemRepository itemRepository,
            IRegisteredUserRepository registeredUserRepository, 
            IAdminRepository adminRepository)
        {
            _transactionRepository = transactionRepository;
            _registeredUserRepository = registeredUserRepository;
            _adminRepository = adminRepository;
            _itemRepository = itemRepository;

        }

        public Result Save(Transaction transaction)
        {
            if (String.IsNullOrEmpty(transaction.BuyerId.ToString()) ||
                String.IsNullOrEmpty(transaction.SellerId.ToString()))
            {
                return Result.Failure("Transaction must have buyer and seller!");
            }
            if (_registeredUserRepository.GetById(transaction.BuyerId) is null ||
                _adminRepository.GetById(transaction.SellerId) is null)
            {
                return Result.Failure("Seller or buyer doesn't exists!");
            }
            _transactionRepository.Save(transaction);
            return Result.Success(transaction);
        }

        public Result SaveUPPItemTransaction(UPPItemTransaction uppItemTransaction)
        {
            List<TransactionItem> transactionItems = new List<TransactionItem>();
            Transaction transaction = new Transaction(Guid.NewGuid(), TransactionStatus.Success, DateTime.Now, 1640, new Guid("12345678-1234-1234-1234-123412341234"), new Guid("12345678-1234-1234-1234-123412341999"));
            foreach (NewSupplyResponseProduct newSupplyResponseProduct in uppItemTransaction.newReviewedSupplyResponse.newSupplyResponse.newSupplyResponseProduct)
            {
                Item item = _itemRepository.GetItemByName(newSupplyResponseProduct.productName);
                transactionItems.Add(new TransactionItem(Guid.NewGuid(), TransactionItemType.Item, item.ProductKey, item.Name, 8, item.Price, transaction.Id));
            }
            transaction.TransactionItems = transactionItems;
            _transactionRepository.Save(transaction);
            return Result.Success(uppItemTransaction);
        }

        public Result EditStatus(TransactionDTO transactionDTO)
        {
            Transaction transaction = _transactionRepository.GetById(transactionDTO.MerchantOrderId);
            bool result = Enum.TryParse(transactionDTO.TransactionStatus, out TransactionStatus transactionStatus);
            if (!result)
            {
                return Result.Failure("Inappropriate Transaction Status!");
            }
            transaction.Status = transactionStatus;
            _transactionRepository.Edit(transaction);
            return Result.Success(transaction);
        }
    }
}
