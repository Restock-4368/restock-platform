namespace Restock.Platform.API.Resource.Domain.Model.Entities;

public class Supply
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Perishable { get; set; }
    public int MinStock { get; set; }
    public int MaxStock { get; set; }
    public int CategoryId { get; set; }
    public int UnitMeasurementId { get; set; }
    public decimal Price { get; set; }
    public int UserId { get; set; }
 
    // Constructor
    public Supply(int id, string name, string description, bool perishable, int minStock, int maxStock,
        int categoryId, int unitMeasurementId, decimal price, int userId)
    {
        Id = id;
        Name = name;
        Description = description;
        Perishable = perishable;
        MinStock = minStock;
        MaxStock = maxStock;
        CategoryId = categoryId;
        UnitMeasurementId = unitMeasurementId;
        Price = price;
        UserId = userId;
    }
    
}
