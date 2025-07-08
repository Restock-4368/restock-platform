namespace Restock.Platform.API.Profiles.Domain.Model.Entities;

public class BusinessCategory
{
    public int Id { get; set; }
    public string Name { get; set; } 
    
     
    public BusinessCategory() { }
 
    public BusinessCategory(string name)
    { 
        Name = name; 
    }
}