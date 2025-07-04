using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Domain.Model.Entities;

namespace Restock.Platform.API.Resource.Domain.Model.Aggregates;
 
public class CustomSupply
{
    public int Id { get; set; }
    public int SupplyId { get; set; }
    public Supply Supply { get; set; } 
    public string Description { get; set; }
    public int MinStock { get; set; }
    public int MaxStock { get; set; } 
    public decimal Price { get; set; }
    public int UserId { get; set; }
 
    // Constructor
    private CustomSupply() { }

    public CustomSupply(int supplyId, string description, int minStock, int maxStock, decimal price, int userId)
    {
        Id = 0; 
        SupplyId = supplyId;

        Description = description;
        MinStock = minStock;
        MaxStock = maxStock; 
        Price = price;
        UserId = userId;
    }
    
    public CustomSupply(CreateCustomSupplyCommand command) : 
        this(
            command.SupplyId,
            command.Description,
            command.MinStock,
            command.MaxStock, 
            command.Price,
            command.UserId
            )
    {}
    
    public void Update( 
        string description,  
        int minStock, 
        int maxStock,   
        decimal price)
    {   
        this.Description = description;
        this.MinStock = MinStock;
        this.MaxStock = MaxStock;
        this.Price = Price;
    }
}
