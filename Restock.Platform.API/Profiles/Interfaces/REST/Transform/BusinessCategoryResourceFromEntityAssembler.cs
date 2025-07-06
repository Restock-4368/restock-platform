using Restock.Platform.API.Profiles.Domain.Model.Entities;
using Restock.Platform.API.Profiles.Interfaces.REST.Resources;

namespace Restock.Platform.API.Profiles.Interfaces.REST.Transform;

public class BusinessCategoryResourceFromEntityAssembler
{
    public static BusinessCategoryResource ToResourceFromEntity(BusinessCategory? entity)
    {
        return new BusinessCategoryResource(entity.Id, entity.Name);
    }
}