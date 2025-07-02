namespace Restock.Platform.API.Resource.Domain.Model.Entities;

public class OrderToSupplierBatch
{ 
    public int OrderId { get; set; }
    public int BatchId { get; set; }
    
    public Batch? Batch { get; set; }
    public double Quantity { get; set; }
    public bool Accepted { get; set; }

    // Constructor
    public OrderToSupplierBatch(int orderId, int batchId, double quantity)
    { 
        OrderId = orderId;
        BatchId = batchId;
        Quantity = quantity;
        Accepted = false;
    }
}