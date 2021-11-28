using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;

namespace WebShop.Core.Services
{
    public class AdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;

        public AdminService(IAdminRepository adminRepository,
            IUserRepository userRepository)
        {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
        }

        public Admin Register(Admin admin)
        {
            if (_userRepository.GetByUsername(admin.Username) != null)
            {
                return null;
            }
            return _adminRepository.Save(admin);
        }
    }
}
