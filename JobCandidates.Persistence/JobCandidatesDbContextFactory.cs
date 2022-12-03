using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidates.Persistence
{
    public class JobCandidatesDbContextFactory : DesignTimeDbContextFactoryBase<JobCandidatesContext>
    {
        protected override JobCandidatesContext CreateNewInstance(DbContextOptions<JobCandidatesContext> options)
        {
            return new JobCandidatesContext(options);
        }
    }
   
}
