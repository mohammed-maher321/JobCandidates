using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidates.Application.UserProfileUseCases.Queries
{
    public class GetUserProfileModel : IRequest<GettUserProfileDto>
    {
        public long Id { get; set; }
    }
}
