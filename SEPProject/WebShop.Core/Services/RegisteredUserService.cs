using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;

namespace WebShop.Core.Services
{
    public class RegisteredUserService
    {
        private readonly IRegisteredUserRepository _registeredUserRepository;
        private readonly IUserRepository _userRepository;

        public RegisteredUserService(IRegisteredUserRepository registeredUserRepository, 
            IUserRepository userRepository)
        {
            _registeredUserRepository = registeredUserRepository;
            _userRepository = userRepository;
        }

        public RegisteredUser Register(RegisteredUser registeredUser)
        {
            if (_userRepository.GetByUsername(registeredUser.Username)!= null)
            {
                return null;
            }
            return _registeredUserRepository.Save(registeredUser);
        }
    }
}
