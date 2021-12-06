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
            if (String.IsNullOrEmpty(course.Name) || String.IsNullOrEmpty(course.Price.ToString()) ||
                String.IsNullOrEmpty(course.ImagePath))
            {
                return Result.Failure("Name, price or image can't be empty!");
            }
            if (course.StartDate == DateTime.MinValue || course.EndDate == DateTime.MinValue)
            {
                return Result.Failure("Invalid date!");
            }
            if (course.Price < 0)
            {
                return Result.Failure("Invalid price!");
            }
            _courseRepository.Save(course);
            return Result.Success(course);
        }
    }
}
