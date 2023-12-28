using MyWebsiteApi.Models;

namespace MyWebsiteApi.Services
{
    public interface IProfileDataService
    {
        Task<ProfileData?> GetProfileDataAsync(string profileId);
    }
}