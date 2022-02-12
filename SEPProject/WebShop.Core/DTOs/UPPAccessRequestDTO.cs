using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Core.DTOs
{
    public class UPPAccessRequestDTO
    {
        public string accessPlace { get; set; }
        public string accessTimestamp { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string education { get; set; }
        public string cv { get; set; }
        public string motivationalLetter { get; set; }
        public string speakingLanguage { get; set; }

        public UPPAccessRequestDTO()
        {
        }

        public UPPAccessRequestDTO(string accessPlace, string accessTimestamp)
        {
            this.accessPlace = accessPlace;
            this.accessTimestamp = accessTimestamp;
        }

        public UPPAccessRequestDTO(string accessPlace, string accessTimestamp, string firstName, string lastName, string email, string address, string education, string cv, string motivationalLetter, string speakingLanguage) : this(accessPlace, accessTimestamp)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.address = address;
            this.education = education;
            this.cv = cv;
            this.motivationalLetter = motivationalLetter;
            this.speakingLanguage = speakingLanguage;
        }
    }
}
