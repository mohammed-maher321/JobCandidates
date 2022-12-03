using JobCandidates.Domain.Entites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidates.Application.UserProfileUseCases.Commands
{
    public class UpdateUserProfileModel : IRequest<UpdateUserProfileDto>
    {
        public List<KeyValuePair<string,Stream>> Files { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
