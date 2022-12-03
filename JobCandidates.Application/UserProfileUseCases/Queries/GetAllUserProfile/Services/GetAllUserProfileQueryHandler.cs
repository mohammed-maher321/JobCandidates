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
    internal class GetAllUserProfileQueryHandler : IRequestHandler<GetAllUserProfilesModel, GettAllUserProfilesListDto>
    {
        private readonly IJobCandidatesContext _databaseContext;


        public GetAllUserProfileQueryHandler(IJobCandidatesContext databaseContext)
        {
            _databaseContext = databaseContext;
        }


        public async Task<GettAllUserProfilesListDto> Handle(GetAllUserProfilesModel request, CancellationToken cancellationToken)
        {

            IQueryable<UserProfile> records = _databaseContext.UserProfile.Include(s => s.UserDocuments).Where(s =>
                string.IsNullOrWhiteSpace(request.Keyword) ? true :
                s.Email.Contains(request.Keyword) ||
                s.FirstName.Contains(request.Keyword) ||
                s.LastName.Contains(request.Keyword)
            );
            int recordCount = records.Count();

            GettAllUserProfilesListDto userProfileList = new GettAllUserProfilesListDto();
            if (records != null && records.Any())
            {
                userProfileList.RecordCount = recordCount;
                userProfileList.UserProfileList = records.OrderByDescending(s => s.Id).Skip(request.Skip).Take(request.Take).ToList();
            }

            return userProfileList;
        }
    }
}
