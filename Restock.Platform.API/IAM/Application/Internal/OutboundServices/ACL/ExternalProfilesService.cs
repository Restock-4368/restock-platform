using Restock.Platform.API.Profiles.Application.ACL;
using Restock.Platform.API.Profiles.Interfaces.ACL;

namespace Restock.Platform.API.IAM.Application.Internal.OutboundServices.ACL;


public class ExternalProfilesService
{
    private readonly IProfilesContextFacade _profilesContextFacade;

    public ExternalProfilesService(IProfilesContextFacade profilesContextFacade)
    {
        _profilesContextFacade = profilesContextFacade;
    }

    public async Task<int> CreateBusiness()
    {
        var businessId = await _profilesContextFacade.CreateBusiness();
         
        return businessId;
    }

    public async Task CreateProfile(int userId, int businessId)
    {
        await _profilesContextFacade.CreateProfile(userId, businessId);
    }
}