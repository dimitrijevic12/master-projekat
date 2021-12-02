using PSP.Core.DTOs;
using PSP.Core.Interface.Repository;
using PSP.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Core.Services
{
    public class PaymentTypeRegisteredWebShopService
    {
        private readonly IPaymentTypeRegisteredWebShopRepository _paymentTypeRegisteredWebShopRepository;

        public PaymentTypeRegisteredWebShopService(IPaymentTypeRegisteredWebShopRepository paymentTypeRegisteredWebShopRepository)
        {
            _paymentTypeRegisteredWebShopRepository = paymentTypeRegisteredWebShopRepository;
        }

        public void Save(PaymentTypeRegisteredWebShopDTO paymentTypeRegisteredWebShopDTO)
        {
            DeletePaymentTypesForRegisteredWebShop(paymentTypeRegisteredWebShopDTO.RegisteredWebShopId);

            if (paymentTypeRegisteredWebShopDTO.PayPal.Item2)
                _paymentTypeRegisteredWebShopRepository.Save(new PaymentTypeRegisteredWebShop(paymentTypeRegisteredWebShopDTO.PayPal.Item1, paymentTypeRegisteredWebShopDTO.RegisteredWebShopId));

            if (paymentTypeRegisteredWebShopDTO.CryptoValute.Item2) 
               _paymentTypeRegisteredWebShopRepository.Save(new PaymentTypeRegisteredWebShop(paymentTypeRegisteredWebShopDTO.CryptoValute.Item1, paymentTypeRegisteredWebShopDTO.RegisteredWebShopId));
            
            if (paymentTypeRegisteredWebShopDTO.Bank.Item2) 
                _paymentTypeRegisteredWebShopRepository.Save(new PaymentTypeRegisteredWebShop(paymentTypeRegisteredWebShopDTO.Bank.Item1, paymentTypeRegisteredWebShopDTO.RegisteredWebShopId));
        }

        private void DeletePaymentTypesForRegisteredWebShop(Guid id)
        {
            foreach (PaymentTypeRegisteredWebShop paymentTypeRegisteredWebShop in _paymentTypeRegisteredWebShopRepository.GetAll())
            {
                if (paymentTypeRegisteredWebShop.RegisteredWebShopId == id) _paymentTypeRegisteredWebShopRepository.Delete(paymentTypeRegisteredWebShop);
            }
        }     
    }
}
