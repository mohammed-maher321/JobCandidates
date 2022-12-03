using JobCandidates.Domain.Entites;
using JobCandidates.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidates.Application.UserProfileUseCases.Queries
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileModel, GettUserProfileDto>
    {
        private readonly IJobCandidatesContext _databaseContext;


        public GetUserProfileQueryHandler(IJobCandidatesContext databaseContext)
        {
            _databaseContext = databaseContext;
        }


        public async Task<GettUserProfileDto> Handle(GetUserProfileModel request, CancellationToken cancellationToken)
        {

            UserProfile? record = await _databaseContext.UserProfile.Include(s => s.UserDocuments).Where(s =>
                s.Id == request.Id).FirstOrDefaultAsync();

            GettUserProfileDto userProfile = new GettUserProfileDto();
            if (record != null)
            {
                userProfile.UserProfile = record;
                return userProfile;

            }
            return null;
        }
    }
}
