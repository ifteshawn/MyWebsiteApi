using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using MyWebsiteApi.Services;

namespace MyWebsiteApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileDataController : ControllerBase
    {
        private readonly IProfileDataService _profileDataService;
        public ProfileDataController(IProfileDataService profileDataService)
        {
            _profileDataService = profileDataService;
        }

        // GET: api/ProfileData
        [HttpGet("{profileId:length(1,50)}")]
        public async Task<IActionResult> GetProfileData(string profileId)
        {
            if(string.IsNullOrWhiteSpace(profileId))
            {
                return BadRequest("ProfileId is required");
            }

            profileId = profileId.ToUpper();

            try
            {
                var profileData = await _profileDataService.GetProfileDataAsync(profileId);
                return Ok(profileData);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }
        }
    }
}
