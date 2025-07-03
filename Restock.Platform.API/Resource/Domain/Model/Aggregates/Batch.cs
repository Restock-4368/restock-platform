using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Domain.Model.Commands;

namespace Restock.Platform.API.Resource.Domain.Model.Entities;

public class Batch
{
    public int Id { get; private set; }
    public int CustomSupplyId { get; private set; }
    public CustomSupply CustomSupply { get; set; }
    public int Stock { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public int UserId { get; private set; } 

    
    // Constructor
    private Batch() { }
    public Batch( int customSupplyId, int stock, DateTime? expirationDate, int userId)
    {
        Id = 0; 
        CustomSupplyId = customSupplyId;
        Stock = stock;
        ExpirationDate = expirationDate;
        UserId = userId; 
    }

    public Batch(CreateBatchCommand command) : 
        this(
            command.CustomSupplyId,
            command.Stock, 
            command.ExpirationDate, 
            command.UserId)
    {}

    public void Update(int stock, DateTime? expirationDate)
    {   
        Stock = stock;
        ExpirationDate = expirationDate;
    }
}
