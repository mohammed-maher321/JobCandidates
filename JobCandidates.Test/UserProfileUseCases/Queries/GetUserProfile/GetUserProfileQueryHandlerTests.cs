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
    public class GetUserProfileQueryHandlerTests
    {
        protected readonly JobCandidatesContext _context;

        public GetUserProfileQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetUserProfileDetail()
        {
            var sut = new GetUserProfileQueryHandler(_context);

            var result = await sut.Handle(new GetUserProfileModel { Id = 1 }, CancellationToken.None);

            result.ShouldBeOfType<GettUserProfileDto>();
        }
    }
}
