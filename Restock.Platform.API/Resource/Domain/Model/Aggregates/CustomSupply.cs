using Restock.Platform.API.Resource.Domain.Model.ValueObjects;

namespace Restock.Platform.API.Resource.Domain.Model.Aggregates;
 
public class CustomSupply
{
    public int Id { get; set; }
    public int SupplyId { get; set; }
    public Supply Supply { get; set; } 
    public string Description { get; set; }
    public bool Perishable { get; set; }
    public int MinStock { get; set; }
    public int MaxStock { get; set; }
    public int CategoryId { get; set; }
    public decimal Price { get; set; }
    public int UserId { get; set; }
 
    // Constructor
    public CustomSupply(Supply supply, string description, bool perishable, int minStock, int maxStock, int categoryId, decimal price, int userId)
    {
        Id = 0;
        Supply = supply ?? throw new ArgumentNullException(nameof(supply));
        SupplyId = supply.Id;

        Description = description;
        Perishable = perishable;
        MinStock = minStock;
        MaxStock = maxStock;
        CategoryId = categoryId;
        Price = price;
        UserId = userId;
    }
}
