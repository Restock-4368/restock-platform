using Restock.Platform.API.Resource.Domain.Model.Aggregates;

namespace Restock.Platform.API.Resource.Domain.Model.Entities;

public class Batch
{
    public int BatchId { get; private set; }
    public int CustomSupplyId { get; private set; }
    
    public CustomSupply CustomSupply { get; set; }
    public int Stock { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public int UserId { get; private set; } 

    
    // Constructor
    public Batch( int customSupplyId, CustomSupply customSupply, int stock, DateTime? expirationDate, int userId)
    {
        BatchId = 0;
        CustomSupply = customSupply;
        CustomSupplyId = customSupplyId;
        Stock = stock;
        ExpirationDate = expirationDate;
        UserId = userId; 
    }
}
