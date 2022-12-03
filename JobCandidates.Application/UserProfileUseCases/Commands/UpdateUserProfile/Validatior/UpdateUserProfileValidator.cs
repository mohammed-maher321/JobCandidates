using FluentValidation;
using JobCandidates.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JobCandidates.Application.UserProfileUseCases.Commands
{
    public class UpdateUserProfileValidator : AbstractValidator<UpdateUserProfileModel>, IUpdateUserProfileValidator
    {
        public UpdateUserProfileValidator()
        {
            RuleFor(x => x.UserProfile.Id).NotEmpty().NotNull().WithMessage("ID Is Required for updating");

            RuleFor(x => x.UserProfile.FirstName).NotEmpty().NotNull().WithMessage("First Name Is Required")
                                                 .MaximumLength(75).WithMessage("First Name Length is 75 character ");

            RuleFor(x => x.UserProfile.LastName).NotEmpty().NotNull().WithMessage("Last Name Is Required")
                                                .MaximumLength(75).WithMessage("Last Name Length is 75 character");

            RuleFor(x => x.UserProfile.Email).NotNull().WithMessage("Email Is Required")
                                             .MaximumLength(200).WithMessage("Email Length is 200 character")
                                             .EmailAddress().WithMessage("A valid email is required");


            RuleFor(x => x.UserProfile).Must(x => EmailUnique(x.Email,x.Id)).WithMessage("This Email already exists."); 

            RuleFor(x => x.UserProfile.Phone).MaximumLength(15).WithMessage("Phone Length is 15 character")
                                             .Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")).WithMessage("Phone Number not valid");

            RuleFor(x => x.UserProfile.LinkedInProfile).MaximumLength(300).WithMessage("LinkedIn Profile Length is 300 characters");
            RuleFor(x => x.UserProfile.GitHubProfile).MaximumLength(300).WithMessage("GitHub Profile Length is 300 characters");


            RuleFor(x => x.UserProfile.CallFrom).GreaterThan(DateTime.Now).WithMessage("Call From Must be Greater Than Date Time Now");
            RuleFor(x => x.UserProfile.CallTo).GreaterThan(DateTime.Now).WithMessage("Call To Must be Greater Than Date Time Now")
                                              .GreaterThan(x => x.UserProfile.CallFrom).WithMessage("Call To Must be Greater Than Call From");



            RuleForEach(x => x.UserProfile.UserDocuments).ChildRules(docs =>
            {
                docs.RuleFor(x => x.FileName).Must(FileExtention).WithMessage("File Extesion must be .csv");
            });


        }

        private bool EmailUnique(string email, long id)
        {
            using(JobCandidatesContext context = new JobCandidatesContext())
            {
                return context.UserProfile.Where(s => s.Email.ToLower() == email.ToLower() && s.Id != id).Count() == 0;
            }
           
        }

        private bool FileExtention(string fileName)
        {
                return fileName.Contains(".csv");

        }
    }
}
