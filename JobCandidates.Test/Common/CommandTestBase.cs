using JobCandidates.Persistence;
using System;

namespace JobCandidates.Test.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly JobCandidatesContext _context;

        public CommandTestBase()
        {
            _context = JobCandidatesContextFactory.Create();
        }

        public void Dispose()
        {
            JobCandidatesContextFactory.Destroy(_context);
        }
    }
}