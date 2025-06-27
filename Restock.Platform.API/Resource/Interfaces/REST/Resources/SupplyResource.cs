namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;

public record SupplyResource(
    int Id, 
    string Name, 
    string Description, 
    string UniMeasurement
);
