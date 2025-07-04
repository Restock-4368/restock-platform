namespace Restock.Platform.API.Resource.Domain.Model.Entities;

public class Supply
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public bool Perishable { get; set; }
    
    public string UnitName { get; set; }

    public string UnitAbbreviation { get; set; }
    
    public string Category { get; set; }
     
    public Supply() { }
 
    public Supply(int id, string name, string description, bool perishable, string unitName, string unitAbbreviation, string category)
    {
        Id = id;
        Name = name;
        Description = description;
        Perishable = perishable;
        UnitName = unitName;
        UnitAbbreviation = unitAbbreviation;
        Category = category;
    }
    
}
