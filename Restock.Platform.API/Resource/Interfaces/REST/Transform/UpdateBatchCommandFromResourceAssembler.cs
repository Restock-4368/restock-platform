using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Interfaces.REST.Resources;

namespace Restock.Platform.API.Resource.Interfaces.REST.Transform;

public static class UpdateBatchCommandFromResourceAssembler
{
    public static UpdateBatchCommand ToCommandFromResource(
        int batchId,
        UpdateBatchResource batchResource)
    {
        return new UpdateBatchCommand( 
            batchId,
            batchResource.Stock, 
            batchResource.ExpirationDate
        );
    }
}