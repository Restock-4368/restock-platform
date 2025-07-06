using Restock.Platform.API.Profiles.Domain.Model.Commands;
using Restock.Platform.API.Profiles.Domain.Model.Queries;
using Restock.Platform.API.Profiles.Domain.Services;
using Restock.Platform.API.Profiles.Interfaces.ACL;

namespace Restock.Platform.API.Profiles.Application.ACL;

public class ProfilesContextFacade(
    IProfileCommandService profileCommandService,
    IProfileQueryService profileQueryService,
    IBusinessCommandService businessCommandService,
    IBusinessQueryService businessQueryService)
    : IProfilesContextFacade
{
    public async Task CreateProfile(int userId, int businessId)
    {
        var createProfileCommand = new CreateProfileCommand(
            userId,
            businessId);
        
        await profileCommandService.Handle(createProfileCommand);
         
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
    
    public async Task<int> CreateBusiness()
    {
        var createBusinessCommand = new CreateBusinessCommand();
        var business = await businessCommandService.Handle(createBusinessCommand);
        return business?.Id ?? 0;
    }

    public async Task DeleteBusiness(int businessId)
    {
        var command = new DeleteBusinessCommand(businessId);
        await businessCommandService.Handle(command);
    }
}