namespace Restock.Platform.API.Resource.Domain.Model.Entities;

public class OrderToSupplierBatch
{ 
    public int OrderToSupplierId { get; set; }
    public int BatchId { get; set; }
    
    public Batch? Batch { get; set; }
    public double Quantity { get; set; }
    public bool Accepted { get; set; }

    // Constructor
    public OrderToSupplierBatch(int orderToSupplierId, int batchId, double quantity, bool accepted)
    { 
        OrderToSupplierId = orderToSupplierId;
        BatchId = batchId;
        Quantity = quantity;
        Accepted = accepted;
    }
    

}