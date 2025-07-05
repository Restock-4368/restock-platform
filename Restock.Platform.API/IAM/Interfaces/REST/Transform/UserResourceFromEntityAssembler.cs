using Restock.Platform.API.IAM.Domain.Model.Aggregates;
using Restock.Platform.API.IAM.Interfaces.REST.Resources;

namespace Restock.Platform.API.IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user)
    {
        return new UserResource(user.Id, user.Username);
    }
}