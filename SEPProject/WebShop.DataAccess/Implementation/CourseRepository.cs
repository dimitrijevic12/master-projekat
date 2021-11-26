using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;
using WebShop.DataAccess.WebShopDbContext;

namespace WebShop.DataAccess.Implementation
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private AppDbContext dbContext;

        public CourseRepository(AppDbContext context) : base(context)
        {
            dbContext = context;
        }
    }
}
