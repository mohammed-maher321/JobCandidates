using JobCandidates.API.InputModels;
using JobCandidates.Application.UserProfileUseCases.Commands;
using JobCandidates.Application.UserProfileUseCases.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobCandidates.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = "UserProfile")]
    public class UserProfileController : BaseController
    {
        [AllowAnonymous]
        [HttpPost]

        public async Task<IActionResult> AddUserProfile([FromForm] AddUserProfileIM addUserProfileModel)
        {
            AddUserProfileModel commandModel = addUserProfileModel.Map();
            var response = await Mediator.Send(commandModel);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserProfileIM updateUserProfileModel)
        {
            UpdateUserProfileModel commandModel = updateUserProfileModel.Map();
            var response = await Mediator.Send(commandModel);
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetUserProfile(long Id)
        {
            GetUserProfileModel queryModel = new GetUserProfileModel() { Id = Id };
            var response = await Mediator.Send(queryModel);
            return Ok(response);
        }




        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllUserProfiles(int skip, int take,string? keyword)
        {
            GetAllUserProfilesModel queryModel = new GetAllUserProfilesModel() { Skip = skip, Take = take, Keyword = keyword };
            var response = await Mediator.Send(queryModel);
            return Ok(response);
        }


    }
}
