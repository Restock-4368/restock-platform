using Restock.Platform.API.IAM.Domain.Model.Commands;
using Restock.Platform.API.IAM.Interfaces.REST.Resources;

namespace Restock.Platform.API.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Username, resource.Password);
    }
}