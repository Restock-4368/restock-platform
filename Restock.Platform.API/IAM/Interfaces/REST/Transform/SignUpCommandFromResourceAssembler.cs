using Restock.Platform.API.IAM.Domain.Model.Commands;
using Restock.Platform.API.IAM.Interfaces.REST.Resources;

namespace Restock.Platform.API.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Username, resource.Password);
    }
}