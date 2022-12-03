using JobCandidates.Application.Common;
using JobCandidates.Application.UserProfileUseCases.Commands;
using JobCandidates.Test.Common;
using MediatR;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.IO;
using System.Threading;
using Xunit;

namespace JobCandidates.Test.UserProfileUseCases.Commands.AddUserProfile
{
    public class UpdateUserProfileCommandTest : CommandTestBase
    {
        [Fact]
        public void Handle_UpdateUserProfile()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var basePath = Directory.GetCurrentDirectory() + string.Format("{0}", Path.DirectorySeparatorChar);
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
               .SetBasePath(basePath)
               .AddJsonFile("appsettings.json")
               .AddJsonFile($"appsettings.Local.json", optional: true)
               .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
               .AddEnvironmentVariables()
               .Build();

            var sut = new UpdateUserProfileCommandHandler(_context, new AttachmentService(configuration), mediatorMock.Object);

            // Act
            var result = sut.Handle(new UpdateUserProfileModel()
            {
                UserProfile = new Domain.Entites.UserProfile()
                {
                    Id = 1,
                    CallFrom = DateTime.Now.AddDays(2),
                    CallTo = DateTime.Now.AddDays(3),
                    Email = "mohammed.maher@mailinator.com",
                    FirstName = "Mohammed",
                    LastName = "Maher",
                    GitHubProfile = "https://github.com",
                    LinkedInProfile = "https://LinkedIn.com",
                    Phone = "01112222332"
                },
            }, CancellationToken.None);


            // Assert
            Assert.True(result.Result != null);
        }
    }
}
