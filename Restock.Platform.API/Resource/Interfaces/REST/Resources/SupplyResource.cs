namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;

public record SupplyResource(
    int SupplyId, 
    string Name, 
    string Description, 
    string UniMeasurement
);
