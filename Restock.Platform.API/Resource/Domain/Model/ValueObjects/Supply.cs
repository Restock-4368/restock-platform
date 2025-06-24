namespace Restock.Platform.API.Resource.Domain.Model.ValueObjects;

public class Supply
{
    public int SupplyId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string UnitMeasurement { get; set; }
    
    // Constructor
    public Supply(int supplyId, string name, string description, string unitMeasurement)
    {
        SupplyId = supplyId;
        Name = name;
        Description = description;
        UnitMeasurement = unitMeasurement; 
    }
    
}
