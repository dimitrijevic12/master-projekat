using WebShop.Core.Model;

namespace WebShop.Core.Interface.Repository
{
    public interface IUserRepository
    {
        public User GetByUsername(string username);
    }
}
