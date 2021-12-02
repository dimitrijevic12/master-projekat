using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Course> GetCoursesForOwner(Guid ownerId)
        {
            return dbContext.Courses.ToList().Where(course => course.OwnerId == ownerId).ToList();
        }
    }
}
