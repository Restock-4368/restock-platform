using Restock.Platform.API.Profiles.Domain.Model.Commands;
using Restock.Platform.API.Profiles.Domain.Model.Queries;
using Restock.Platform.API.Profiles.Domain.Services;
using Restock.Platform.API.Profiles.Interfaces.ACL;

namespace Restock.Platform.API.Profiles.Application.ACL;

public class ProfilesContextFacade(
    IProfileCommandService profileCommandService,
    IProfileQueryService profileQueryService)
    : IProfilesContextFacade
{
    public async Task<int> CreateProfile(string firstName, string lastName, string email, 
        string phone, string address, string country, int userId, int businessId)
    {
        var createProfileCommand = new CreateProfileCommand(
            firstName,
            lastName,
            email,
            phone,
            address,
            country,
            userId,
            businessId);
        var profile = await profileCommandService.Handle(createProfileCommand);
        return profile?.Id ?? 0;
    }

    public async Task DeleteProfile(int profileId)
    {
        var command = new DeleteProfileCommand(profileId);
        await profileCommandService.Handle(command);
    }
    
    public async Task<int> FetchProfileIdByEmail(string email)
    {
        var getProfileByEmailQuery = new GetProfileByEmailQuery(email);
        var profile = await profileQueryService.Handle(getProfileByEmailQuery);
        return profile?.Id ?? 0;
    }
}