using JobCandidates.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidates.Persistence.Configurations
{
    public class UserDocumentConfiguration : IEntityTypeConfiguration<UserDocument>
    {
        public void Configure(EntityTypeBuilder<UserDocument> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
            entity.HasOne(d => d.UserProfile)
               .WithMany(p => p.UserDocuments)
               .HasForeignKey(d => d.UserId)
               .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
