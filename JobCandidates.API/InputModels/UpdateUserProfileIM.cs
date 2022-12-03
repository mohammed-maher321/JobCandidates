using JobCandidates.Application.UserProfileUseCases.Commands;

namespace JobCandidates.API.InputModels
{
    public class UpdateUserProfileIM
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CallFrom { get; set; }
        public DateTime CallTo { get; set; }
        public string GitHubProfile { get; set; }
        public string LinkedInProfile { get; set; }
        public IFormFile[] Files { get; set; }
        public UpdateUserProfileModel Map()
        {
            UpdateUserProfileModel model = new UpdateUserProfileModel()
            {
                UserProfile = new Domain.Entites.UserProfile()
                {
                    Id = Id,
                    CallFrom = CallFrom,
                    CallTo = CallTo,
                    Email = Email,
                    FirstName = FirstName,
                    GitHubProfile = GitHubProfile,
                    LastName = LastName,
                    LinkedInProfile = LinkedInProfile,
                    Phone = Phone,
                    UserDocuments = Files?.Select(s => new Domain.Entites.UserDocument()
                    {
                        FileName = s.FileName,
                        FilePath = Guid.NewGuid().ToString()
                    }).ToList(),
                }
                
                
            };
            model.Files = model.UserProfile?.UserDocuments?.Select(s => new KeyValuePair<string, Stream>(s.FileName, Files?.FirstOrDefault(s => s.FileName == s.FileName).OpenReadStream())).ToList();
            return model;
        }
    }
}
