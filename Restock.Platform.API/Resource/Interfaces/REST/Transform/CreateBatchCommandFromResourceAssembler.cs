using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Interfaces.REST.Resources;

namespace Restock.Platform.API.Resource.Interfaces.REST.Transform;

public static class CreateBatchCommandFromResourceAssembler
{
    public static CreateBatchCommand ToCommandFromResource(CreateBatchResource resource)
    {
        return new CreateBatchCommand(   
            resource.CustomSupplyId, 
            resource.Stock,
            resource.ExpirationDate,
            resource.UserId
        );
    }
}