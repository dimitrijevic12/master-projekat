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
            if (String.IsNullOrEmpty(conference.Name) || String.IsNullOrEmpty(conference.Price.ToString()))
            {
                return Result.Failure("Name or price can't be empty!");
            }
            _conferenceRepository.Save(conference);
            return Result.Success(conference);
        }
    }
}
