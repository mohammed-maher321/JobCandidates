using JobCandidates.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace JobCandidates.Persistence
{
    public interface IJobCandidatesContext
    {
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<UserDocument> UserDocuments { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}