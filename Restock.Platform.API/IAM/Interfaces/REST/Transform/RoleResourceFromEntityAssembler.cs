using Restock.Platform.API.IAM.Domain.Model.Entities;
using Restock.Platform.API.IAM.Interfaces.REST.Resources;

namespace Restock.Platform.API.IAM.Interfaces.REST.Transform;

public class RoleResourceFromEntityAssembler
{
    public static RoleResource ToResourceFromEntity(Role role)
    {
        return new RoleResource(role.Id, role.GetStringName());
    }
}