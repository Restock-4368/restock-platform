using Restock.Platform.API.Profiles.Domain.Model.Commands;
using Restock.Platform.API.Profiles.Interfaces.REST.Resources;

namespace Restock.Platform.API.Profiles.Interfaces.REST.Transform;
 
public static class UpdateBusinessCommandFromResourceAssembler
{
    public static UpdateBusinessCommand ToCommandFromResource(
        int businessId,
        UpdateBusinessResource resource)
    {
        return new UpdateBusinessCommand(  
            businessId,
            resource.Name,
            resource.Email,
            resource.Phone,  
            resource.Address,
            resource.Categories
        );
    }
}