using Restock.Platform.API.Profiles.Domain.Model.Commands;
using Restock.Platform.API.Profiles.Interfaces.REST.Resources;
using Restock.Platform.API.Resource.Domain.Model.Commands;

namespace Restock.Platform.API.Profiles.Interfaces.REST.Transform;
 
public static class UpdateProfileCommandFromResourceAssembler
{
    public static UpdateProfileCommand ToCommandFromResource(
        int profileId,
        UpdateProfileResource resource)
    {
        return new UpdateProfileCommand(  
            profileId,
            resource.FirstName,
            resource.LastName,
            resource.Avatar,
            resource.Email, 
            resource.Phone,
            resource.Address,
            resource.Country
        );
    }
}