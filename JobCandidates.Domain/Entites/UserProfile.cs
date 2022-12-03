using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidates.Domain.Entites
{
    public class UserProfile
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CallFrom { get; set; }
        public DateTime CallTo { get; set; }
        public string GitHubProfile { get; set; }
        public string LinkedInProfile { get; set; }
    }
}
