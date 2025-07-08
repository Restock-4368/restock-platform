using Restock.Platform.API.Profiles.Domain.Model.Commands;
using Restock.Platform.API.Profiles.Interfaces.REST.Resources;

namespace Restock.Platform.API.Profiles.Interfaces.REST.Transform;
 
public static class CreateBusinessCommandFromResourceAssembler
{
    public static CreateBusinessCommand ToCommandFromResource(CreateBusinessResource resource)
    {
        return new CreateBusinessCommand( 
        );
    }
}