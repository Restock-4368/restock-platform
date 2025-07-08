using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Shared.Domain.Exceptions;

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
        if (customSupplyId <= 0)
            throw new BusinessRuleException("CustomSupplyId must be greater than 0.");

        if (stock < 0)
            throw new BusinessRuleException("Stock cannot be negative.");

        if (userId <= 0)
            throw new BusinessRuleException("UserId must be greater than 0.");
 
        if (expirationDate.HasValue && expirationDate.Value < DateTime.UtcNow)
            throw new BusinessRuleException("Expiration date cannot be in the past.");
 
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
