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
    public class RegisteredWebShopService
    {
        private readonly IRegisteredWebShopRepository _registeredWebShopRepository;

        public RegisteredWebShopService(IRegisteredWebShopRepository registeredWebShopRepository)
        {
            _registeredWebShopRepository = registeredWebShopRepository;
        }

        public RegisteredWebShop Save(RegisteredWebShopDTO registeredWebShopDTO)
        {
            return _registeredWebShopRepository.Save(new RegisteredWebShop(registeredWebShopDTO.Id, registeredWebShopDTO.WebShopId, registeredWebShopDTO.WebShopName,
                registeredWebShopDTO.Password, registeredWebShopDTO.EmailAddress, registeredWebShopDTO.SuccessUrl, registeredWebShopDTO.FailedUrl, registeredWebShopDTO.ErrorUrl));
        }

    }
}
