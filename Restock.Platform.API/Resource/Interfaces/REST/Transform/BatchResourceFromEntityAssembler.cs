using Restock.Platform.API.Resource.Domain.Model.Entities;
using Restock.Platform.API.Resource.Interfaces.REST.Resources;

namespace Restock.Platform.API.Resource.Interfaces.REST.Transform;

public static class BatchResourceFromEntityAssembler
{
    public static BatchResource ToResourceFromEntity(Batch? batch)
    {
        return new BatchResource(
            batch.Id,
            batch.CustomSupplyId, 
            batch.CustomSupply != null ? CustomSupplyResourceFromEntityAssembler.ToResourceFromEntity(batch.CustomSupply) : null,
            batch.Stock, 
            batch.ExpirationDate, 
            batch.UserId
            );
    }
}