using Restock.Platform.API.Profiles.Domain.Model.Aggregates;
using Restock.Platform.API.Profiles.Interfaces.REST.Resources;

namespace Restock.Platform.API.Profiles.Interfaces.REST.Transform;
 
public static class ProfileResourceFromEntityAssembler
{
    public static ProfileResource ToResourceFromEntity(Profile entity)
    {
        return new ProfileResource(
            entity.Id, 
            entity.FullName, 
            entity.Avatar.Value,
            entity.Email, 
            entity.Phone, 
            entity.Address, 
            entity.Country,
            entity.UserId.Value,
            entity.BusinessId,
            entity.Business != null ? BusinessResourceFromEntityAssembler.ToResourceFromEntity(entity.Business) : null);
    }
}