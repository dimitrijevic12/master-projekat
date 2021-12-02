using CSharpFunctionalExtensions;
using System;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;

namespace WebShop.Core.Services
{
    public class CourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public Result Save(Course course)
        {
            if (String.IsNullOrEmpty(course.Name) || String.IsNullOrEmpty(course.Price.ToString()))
            {
                return Result.Failure("Name or price can't be empty!");
            }
            _courseRepository.Save(course);
            return Result.Success(course);
        }
    }
}
