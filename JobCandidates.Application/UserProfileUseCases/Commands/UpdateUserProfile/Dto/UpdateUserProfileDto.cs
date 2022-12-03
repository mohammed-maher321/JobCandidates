using JobCandidates.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidates.Application.UserProfileUseCases.Commands
{
    public class UpdateUserProfileDto
    {
        public UserProfile UserProfile { get; set; }
    }
}
