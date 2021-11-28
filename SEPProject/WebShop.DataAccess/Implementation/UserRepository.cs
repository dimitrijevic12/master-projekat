using System.Linq;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;
using WebShop.DataAccess.WebShopDbContext;

namespace WebShop.DataAccess.Implementation
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext dbContext;

        public UserRepository(AppDbContext context)
        {
            dbContext = context;
        }
        public User GetByUsername(string username)
        {
            RegisteredUser registeredUser = dbContext.RegisteredUsers.ToList().FirstOrDefault(registeredUser => registeredUser.Username.Equals(username));
            if (registeredUser == null)
            {
                return dbContext.Admins.ToList().
                    FirstOrDefault(admin => admin.Username.Equals(username));
            }
            return registeredUser;
        }
    }
}
