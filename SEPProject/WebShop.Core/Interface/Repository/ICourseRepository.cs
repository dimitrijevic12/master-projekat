using System;
using System.Collections.Generic;
using WebShop.Core.Model;

namespace WebShop.Core.Interface.Repository
{
    public interface ICourseRepository : IRepository<Course>
    {
        public IEnumerable<Course> GetCoursesForOwner(Guid ownerId);

        public IEnumerable<Course> GetFutureCourses();

        public Course GetByName(string name);
    }
}