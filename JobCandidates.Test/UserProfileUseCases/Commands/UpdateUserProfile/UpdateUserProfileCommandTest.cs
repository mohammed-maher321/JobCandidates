using JobCandidates.Application.Common;
using JobCandidates.Application.UserProfileUseCases.Commands;
using JobCandidates.Test.Common;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
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
            var attachmentServiceMock = new Mock<AttachmentService>();

            var sut = new UpdateUserProfileCommandHandler(_context, attachmentServiceMock.Object, mediatorMock.Object);

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
            Assert.IsNotNull(result.Result);
        }
    }
}
