using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidates.Application.UserProfileUseCases.Queries
{
    internal class GetAllUserProfilesModel : IRequest<GettAllUserProfilesListDto>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string Keyword { get; set; }
    }
}
