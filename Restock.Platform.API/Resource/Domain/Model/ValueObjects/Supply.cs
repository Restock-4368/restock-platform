namespace Restock.Platform.API.Resource.Domain.Model.ValueObjects;

public record Supply
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string UnitMeasurement { get; set; }
    
    // Constructor
    public Supply(int Id, string name, string description, string unitMeasurement)
    {
        Id = Id;
        Name = name;
        Description = description;
        UnitMeasurement = unitMeasurement; 
    }
    
}
