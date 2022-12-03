using JobCandidates.Application.UserProfileUseCases.Queries;
using JobCandidates.Persistence;
using JobCandidates.Test.Common;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobCandidates.Test.UserProfileUseCases.Queries
{
    [Collection("QueryCollection")]
    public class GetAllUserProfileQueryHandlerTests
    {
        protected readonly JobCandidatesContext _context;

        public GetAllUserProfileQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetAllUserProfile()
        {
            var sut = new GetAllUserProfileQueryHandler(_context);

            var result = await sut.Handle(new GetAllUserProfilesModel { Skip = 0 , Take=20  }, CancellationToken.None);

            result.ShouldBeOfType<GettAllUserProfilesListDto>();
        }
    }
}
