using Restock.Platform.API.Profiles.Domain.Model.Aggregates;
using Restock.Platform.API.Profiles.Domain.Model.Entities;
using Restock.Platform.API.Profiles.Interfaces.REST.Resources;

namespace Restock.Platform.API.Profiles.Interfaces.REST.Transform;
 
public static class BusinessResourceFromEntityAssembler
{
    public static BusinessResource ToResourceFromEntity(Business? entity)
    {
        return new BusinessResource(entity.Id, entity.Name, entity.Email, entity.Phone, entity.Address, entity.Categories);
    }
}