using Restock.Platform.API.Resource.Domain.Model.Commands;
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
    private CustomSupply() { }

    public CustomSupply(int supplyId, string description, bool perishable, int minStock, int maxStock, int categoryId, decimal price, int userId)
    {
        Id = 0; 
        SupplyId = supplyId;

        Description = description;
        Perishable = perishable;
        MinStock = minStock;
        MaxStock = maxStock;
        CategoryId = categoryId;
        Price = price;
        UserId = userId;
    }
    
    public CustomSupply(CreateCustomSupplyCommand command) : 
        this(
            command.SupplyId,
            command.Description,
            command.Perishable,
            command.MinStock,
            command.MaxStock,
            command.CategoryId,
            command.Price,
            command.UserId
            )
    {}
    
    public void Update( 
        string description, 
        bool perishable, 
        int minStock, 
        int maxStock,   
        decimal price)
    {   
        this.Description = description;
        this.Perishable = perishable;
        this.MinStock = MinStock;
        this.MaxStock = MaxStock;
        this.Price = Price;
    }
}
