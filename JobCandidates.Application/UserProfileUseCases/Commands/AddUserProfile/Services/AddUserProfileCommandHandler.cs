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
    public class AddUserProfileCommandHandler : IRequestHandler<AddUserProfileModel, AddUserProfileDto>
    {

        private readonly IJobCandidatesContext _databaseContext;
        private readonly IMediator _mediator;
        private readonly IAttachmentService _attachmentService;

        public AddUserProfileCommandHandler(
            IJobCandidatesContext databaseContext,
            IAttachmentService attachmentService,
            IMediator mediator)
        {
            _databaseContext = databaseContext;
            _mediator = mediator;
            _attachmentService = attachmentService;
        }

        public async Task<AddUserProfileDto> Handle(AddUserProfileModel request, CancellationToken cancellationToken)
        {
            foreach (var item in request.Files)
            {
                var fileNameSpiltted = item.Key.Split('.');
                string fileExtension = fileNameSpiltted[fileNameSpiltted.Length - 1];
                List<KeyValuePair<Stream, string>> attachments = new List<KeyValuePair<Stream, string>>();
                string dir = _attachmentService.GetMappedDirectory("UserProfile/{UserProfileEmail}", new Dictionary<string, string>() { { "UserProfileEmail", request.UserProfile.Email } }, UploadTypeEnum.LocalServer) + "/" + request.UserProfile.UserDocuments.FirstOrDefault(s => s.FileName == item.Key).FilePath + "." + fileExtension;
                attachments.Add(new KeyValuePair<Stream, string>(item.Value, dir));
                _attachmentService.UploadAttachment(attachments, UploadTypeEnum.LocalServer);
            }


            await _databaseContext.UserProfile.AddAsync(request.UserProfile);
            await _databaseContext.SaveChangesAsync(cancellationToken);

            
           
            return new AddUserProfileDto() { UserProfile = request.UserProfile };
        }
    }
}
