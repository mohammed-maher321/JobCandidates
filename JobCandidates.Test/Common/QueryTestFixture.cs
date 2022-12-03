using JobCandidates.Persistence;
using System;
using Xunit;

namespace JobCandidates.Test.Common
{
    public class QueryTestFixture : IDisposable
    {
        public JobCandidatesContext Context { get; private set; }

        public QueryTestFixture()
        {
            Context = JobCandidatesContextFactory.Create();
        }

        public void Dispose()
        {
            JobCandidatesContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}