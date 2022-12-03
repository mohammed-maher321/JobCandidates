using FluentValidation.Results;

namespace JobCandidates.Application.UserProfileUseCases.Commands
{
    public interface IAddUserProfileValidator
    {
        public ValidationResult Validate(AddUserProfileModel userProfileModel);
    }
}
