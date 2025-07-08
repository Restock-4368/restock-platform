namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;

public record UpdateOrderResource(  
    DateTime? EstimatedShipDate,
    DateTime? EstimatedShipTime,
    string? Description
);

 
 