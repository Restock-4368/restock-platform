namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;

public record SupplyResource(
    int Id, 
    string Name, 
    string Description, 
    bool Perishable,
    string UnitName,
    string UnitAbbreviation,
    string Category
);
