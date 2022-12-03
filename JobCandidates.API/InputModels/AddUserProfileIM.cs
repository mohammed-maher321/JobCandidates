using JobCandidates.Application.UserProfileUseCases.Commands;

namespace JobCandidates.API.InputModels
{
    public class AddUserProfileIM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CallFrom { get; set; }
        public DateTime CallTo { get; set; }
        public string GitHubProfile { get; set; }
        public string LinkedInProfile { get; set; }
        public virtual List<UserDocumentIM> UserDocuments { get; set; }

        public AddUserProfileModel Map()
        {
            AddUserProfileModel model = new AddUserProfileModel()
            {
                UserProfile = new Domain.Entites.UserProfile()
                {
                    CallFrom = CallFrom,
                    CallTo = CallTo,
                    Email = Email,
                    FirstName = FirstName,
                    GitHubProfile = GitHubProfile,
                    LastName = LastName,
                    LinkedInProfile = LinkedInProfile,
                    Phone = Phone,
                    UserDocuments = UserDocuments.Select(s => new Domain.Entites.UserDocument()
                    {
                        FileName = s.FileName,
                        FilePath = Guid.NewGuid().ToString()
                    }).ToList(),
                }
                
                
            };
            model.Files = model.UserProfile.UserDocuments.Select(s => new KeyValuePair<string, Stream>(s.FileName, UserDocuments.FirstOrDefault(s => s.FileName == s.FileName).File.OpenReadStream())).ToList();
            return model;
        }
    }
}
