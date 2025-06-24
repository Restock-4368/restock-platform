namespace Restock.Platform.API.Resource.Domain.Model.Entities;

public class Batch
{
    public int BatchId { get; private set; }
    public int SupplyId { get; private set; }
    
    public Supply? Supply { get; set; }
    public int Stock { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public int UserId { get; private set; } 

    
    // Constructor
    public Batch(int batchId, int supplyId, int stock, DateTime? expirationDate, int userId)
    {
        BatchId = batchId;
        SupplyId = supplyId;
        Stock = stock;
        ExpirationDate = expirationDate;
        UserId = userId; 
    }

}