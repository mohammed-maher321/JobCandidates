using JobCandidates.Domain.Entites;
using JobCandidates.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace JobCandidates.Test.Common
{
    public class JobCandidatesContextFactory
    {
        public static JobCandidatesContext Create()
        {
            var options = new DbContextOptionsBuilder<JobCandidatesContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new JobCandidatesContext(options);

            context.Database.EnsureCreated();

            context.UserProfile.AddRange(new[] {
                new UserProfile { CallFrom = DateTime.Now.AddDays(1), CallTo = DateTime.Now.AddDays(2), Email = "mohammed.maher@mailinator.com", FirstName = "Mohammed" , LastName = "Maher" , GitHubProfile = "https://github.com" , LinkedInProfile = "https://LinkedIn.com" , Phone = "01112222222" , Id = 1 },
                new UserProfile { CallFrom = DateTime.Now.AddDays(1), CallTo = DateTime.Now.AddDays(2), Email = "mohammed.maher1@mailinator.com", FirstName = "Ahmed" , LastName = "Maher" , GitHubProfile = "https://github.com" , LinkedInProfile = "https://LinkedIn.com" , Phone = "01112222332" , Id = 2 },
                new UserProfile { CallFrom = DateTime.Now.AddDays(1), CallTo = DateTime.Now.AddDays(2), Email = "mohammed.maher2@mailinator.com", FirstName = "Karim" , LastName = "Ahmed" , GitHubProfile = "https://github.com" , LinkedInProfile = "https://LinkedIn.com" , Phone = "01112222442" , Id = 3 },
            });

            context.UserDocuments.AddRange(new[] {
                new UserDocument()
                {
                    Id = 1,
                    UserId = 1,
                    FileName = "Doc1.csv",
                    FilePath = "Doc1",
                },
                new UserDocument()
                {
                    Id = 2,
                    UserId = 2,
                    FileName = "Doc2.csv",
                    FilePath ="Doc2",
                },
                new UserDocument()
                {
                    Id = 3,
                    UserId = 3,
                    FileName = "Doc3.csv",
                    FilePath = "Doc3",
                },
            });
            

            context.SaveChanges();

            return context;
        }

        public static void Destroy(JobCandidatesContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}