using JobCandidates.Domain.Entites;
using JobCandidates.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidates.Persistence
{
    public partial class JobCandidatesContext : DbContext, IJobCandidatesContext 
    {

        public JobCandidatesContext()
        {
        }

        public JobCandidatesContext(DbContextOptions<JobCandidatesContext> options)
            : base(options)
        {
        }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<UserDocument> UserDocuments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=JobCandidates;Persist Security Info=True;User ID=sa;Password=123@eee");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
            modelBuilder.ApplyConfiguration(new UserDocumentConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
