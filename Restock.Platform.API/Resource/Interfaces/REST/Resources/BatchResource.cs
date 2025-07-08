using Restock.Platform.API.Resource.Domain.Model.Aggregates;

namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;
 
public record BatchResource(
    int Id, 
    int CustomSupplyId, 
    CustomSupplyResource CustomSupply, 
    int Stock, 
    DateTime? ExpirationDate, 
    int UserId
);
