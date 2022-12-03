using JobCandidates.Application.Common;
using JobCandidates.Application.UserProfileUseCases.Commands;
using JobCandidates.Test.Common;
using MediatR;
using Moq;
using System;
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
            var attachmentServiceMock = new Mock<AttachmentService>();

            var sut = new AddUserProfileCommandHandler(_context, attachmentServiceMock.Object, mediatorMock.Object);

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
                    Phone = "01112222332"
                },
            }, CancellationToken.None);


            // Assert
            Assert.True(result.Result != null);
        }
    }
}
