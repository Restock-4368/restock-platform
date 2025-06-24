using Restock.Platform.API.Resource.Domain.Model.ValueObjects;

namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;

public record CustomSupplyResource(
    int SupplyId,
    Supply Supply, 
    string Description, 
    bool Perishable, 
    int MinStock, 
    int MaxStock, 
    int CategoryId, 
    decimal Price, 
    int UserId
    );