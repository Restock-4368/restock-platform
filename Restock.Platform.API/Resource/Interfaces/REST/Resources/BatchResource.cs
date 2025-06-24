using Restock.Platform.API.Resource.Domain.Model.Aggregates;

namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;
 
public record BatchResource(
    int BatchId, 
    int CustomSupplyId, 
    CustomSupply CustomSupply, 
    int Stock, 
    DateTime? ExpirationDate, 
    int UserId
);
