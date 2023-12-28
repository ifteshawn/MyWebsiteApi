using Microsoft.Azure.Cosmos;
using MyWebsiteApi.Models;

namespace MyWebsiteApi.Services
{
    public class ProfileDataService : IProfileDataService
    {
        private readonly Container _container;

        public ProfileDataService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<ProfileData?> GetProfileDataAsync(string profileId)
        {
            var query = new QueryDefinition("SELECT * FROM c WHERE c.id = @profileId")
    .WithParameter("@profileId", profileId);

            var iterator = _container.GetItemQueryIterator<ProfileData>(query);
            var profileData = new List<ProfileData>();

            if (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                profileData.AddRange(response.Resource);

            }

            ProfileData? profile = profileData.FirstOrDefault();

            return profile;
        }
    }
}