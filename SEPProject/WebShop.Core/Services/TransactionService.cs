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
        private readonly IConferenceRepository _conferenceRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ITransportationRepository _transportationRepository;
        private readonly IAccommodationRepository _accommodationRepository;

        public TransactionService(ITransactionRepository transactionRepository, IItemRepository itemRepository,
            IRegisteredUserRepository registeredUserRepository, IAdminRepository adminRepository, IConferenceRepository conferenceRepository,
            ICourseRepository courseRepository, ITransportationRepository transportationRepository, IAccommodationRepository accommodationRepository)
        {
            _transactionRepository = transactionRepository;
            _itemRepository = itemRepository;
            _registeredUserRepository = registeredUserRepository;
            _adminRepository = adminRepository;
            _conferenceRepository = conferenceRepository;
            _courseRepository = courseRepository;
            _transportationRepository = transportationRepository;
            _accommodationRepository = accommodationRepository;
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

        public Result SaveUPPEducation(EducationPurchaseOrder educationPurchaseOrder)
        {
            List<TransactionItem> transactionItems = new List<TransactionItem>();
            Transaction transaction = new Transaction(Guid.NewGuid(), TransactionStatus.Success, DateTime.Now, 1640, new Guid("12345678-1234-1234-1234-123412341234"), new Guid("12345678-1234-1234-1234-123412341999"));
            Conference conference = _conferenceRepository.GetByName(educationPurchaseOrder.educationRequest);
            if (conference != null)
            {
                transactionItems.Add(new TransactionItem(Guid.NewGuid(), TransactionItemType.Conference, conference.Id, conference.Name, 1, conference.Price, transaction.Id));
            }
            else
            {
                Course course = _courseRepository.GetByName(educationPurchaseOrder.educationRequest);
                transactionItems.Add(new TransactionItem(Guid.NewGuid(), TransactionItemType.Course, course.Id, course.Name, 1, course.Price, transaction.Id));
            }
            if (educationPurchaseOrder.online == true)
            {
                transaction.TransactionItems = transactionItems;
                _transactionRepository.Save(transaction);
                return Result.Success(transaction);
            }
            Transportation transportation = _transportationRepository.GetTransportationByName(educationPurchaseOrder.transportationOption);
            transactionItems.Add(new TransactionItem(Guid.NewGuid(), TransactionItemType.Transportation, transportation.Id, transportation.Name, 1, transportation.Price, transaction.Id));
            Accommodation accommodation = _accommodationRepository.GetByName(educationPurchaseOrder.accommodationOption);
            transactionItems.Add(new TransactionItem(Guid.NewGuid(), TransactionItemType.Accommodation, accommodation.Id, accommodation.Name, 1, accommodation.CostPerNight * (DateTime.Parse(educationPurchaseOrder.endDate) - DateTime.Parse(educationPurchaseOrder.startDate)).TotalDays, transaction.Id));
            transaction.TransactionItems = transactionItems;
            _transactionRepository.Save(transaction);
            return Result.Success(transaction);
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