using PSP.Core.Interface.Repository;
using PSP.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Core.Services
{
    public class PaymentTypeService
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly ITransactionRepository _transactionRepository;

        public PaymentTypeService(IMerchantRepository merchantRepository, ITransactionRepository transactionRepository)
        {
            _merchantRepository = merchantRepository;
            _transactionRepository = transactionRepository;
        }

        public ICollection<PaymentType> GetPaymentTypesForWebShopByOrderId(Guid orderId)
        {
            Transaction transaction = _transactionRepository.GetTransactionByOrderId(orderId);
            if (transaction == null) return null;
            Merchant merchant = _merchantRepository.GetByMerchantId(transaction.MerchantId);
            if (merchant == null) return null;
            return merchant.RegisteredWebShop.PaymentTypes;
        }
    }
}
