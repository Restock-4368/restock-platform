using Restock.Platform.API.Resource.Domain.Model.ValueObjects;

namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;

public record CustomSupplyResource(
    int Id,
    int SupplyId,
    SupplyResource Supply, 
    string Description,  
    int MinStock, 
    int MaxStock,  
    decimal Price, 
    int UserId
    );