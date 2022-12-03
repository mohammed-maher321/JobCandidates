using FluentValidation.Results;

namespace JobCandidates.Application.UserProfileUseCases.Commands
{
    public interface IUpdateUserProfileValidator
    {
        public ValidationResult Validate(UpdateUserProfileModel userProfileModel);
    }
}
