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
    public class AddUserProfileCommandTest : CommandTestBase
    {
        [Fact]
        public void Handle_AddUserProfile()
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

            var sut = new AddUserProfileCommandHandler(_context, new AttachmentService(configuration), mediatorMock.Object);

            // Act
            var result = sut.Handle(new AddUserProfileModel()
            {
                UserProfile = new Domain.Entites.UserProfile()
                {
                    CallFrom = DateTime.Now.AddDays(1),
                    CallTo = DateTime.Now.AddDays(2),
                    Email = "mohammed.maher3@mailinator.com",
                    FirstName = "Mohammed",
                    LastName = "Maher",
                    GitHubProfile = "https://github.com",
                    LinkedInProfile = "https://LinkedIn.com",
                    Phone = "01112222332",
                },
                Files = new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, Stream>>()
            }, CancellationToken.None);


            // Assert
            Assert.True(result.Result != null);
        }
    }
}
