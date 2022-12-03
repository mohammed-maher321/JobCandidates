using JobCandidates.Application.Common;
using JobCandidates.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidates.Application.UserProfileUseCases.Commands
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileModel, UpdateUserProfileDto>
    {

        private readonly IJobCandidatesContext _databaseContext;
        private readonly IMediator _mediator;
        private readonly IAttachmentService _attachmentService;

        public UpdateUserProfileCommandHandler(
            IJobCandidatesContext databaseContext,
            IAttachmentService attachmentService,
            IMediator mediator)
        {
            _databaseContext = databaseContext;
            _mediator = mediator;
            _attachmentService = attachmentService;
        }

        public async Task<UpdateUserProfileDto> Handle(UpdateUserProfileModel request, CancellationToken cancellationToken)
        {
            var record = _databaseContext.UserProfile.FirstOrDefault(s => s.Id == request.UserProfile.Id);
            if(record != null)
            {
                record.FirstName = request.UserProfile.FirstName;
                record.LastName = request.UserProfile.LastName;
                record.Email = request.UserProfile.Email;
                record.CallFrom = request.UserProfile.CallFrom;
                record.CallTo = request.UserProfile.CallTo;
                record.GitHubProfile = request.UserProfile.GitHubProfile;
                record.LinkedInProfile  = request.UserProfile.LinkedInProfile;
                record.Phone = request.UserProfile.Phone;
            }
            if (request.UserProfile.UserDocuments != null)
            {
                foreach (var item in request.Files)
                {
                    var fileNameSpiltted = item.Key.Split('.');
                    string fileExtension = fileNameSpiltted[fileNameSpiltted.Length - 1];
                    List<KeyValuePair<Stream, string>> attachments = new List<KeyValuePair<Stream, string>>();
                    string dir = _attachmentService.GetMappedDirectory("UserProfile/{UserProfileEmail}", new Dictionary<string, string>() { { "UserProfileEmail", request.UserProfile.Email } }, UploadTypeEnum.LocalServer) + "/" + request.UserProfile.UserDocuments.FirstOrDefault(s => s.FileName == item.Key).FilePath + "." + fileExtension;
                    attachments.Add(new KeyValuePair<Stream, string>(item.Value, dir));
                    _attachmentService.UploadAttachment(attachments, UploadTypeEnum.LocalServer);
                    record.UserDocuments.Add(new Domain.Entites.UserDocument()
                    {
                        FileName = item.Key,
                        FilePath = request.UserProfile.UserDocuments.FirstOrDefault(s => s.FileName == item.Key).FilePath,
                        UserId = record.Id
                    });
                }

                
            }
            await _databaseContext.SaveChangesAsync(cancellationToken);

            return new UpdateUserProfileDto() { UserProfile = record };
        }
    }
}
