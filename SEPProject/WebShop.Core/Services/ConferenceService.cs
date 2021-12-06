using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;

namespace WebShop.Core.Services
{
    public class ConferenceService
    {
        private readonly IConferenceRepository _conferenceRepository;

        public ConferenceService(IConferenceRepository conferenceRepository)
        {
            _conferenceRepository = conferenceRepository;
        }

        public Result Save(Conference conference)
        {
            if (String.IsNullOrEmpty(conference.Name) || String.IsNullOrEmpty(conference.Price.ToString()) ||
                String.IsNullOrEmpty(conference.ImagePath))
            {
                return Result.Failure("Name, price or image can't be empty!");
            }
            if (conference.Date == DateTime.MinValue)
            {
                return Result.Failure("Invalid date!");
            }
            if (conference.Price < 0)
            {
                return Result.Failure("Invalid price!");
            }
            _conferenceRepository.Save(conference);
            return Result.Success(conference);
        }
    }
}
