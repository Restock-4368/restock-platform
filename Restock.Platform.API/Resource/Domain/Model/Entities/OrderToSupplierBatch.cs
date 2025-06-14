namespace Restock.Platform.API.Resource.Domain.Model.Entities;

public class OrderToSupplierBatch
{
    public int Id { get; set; }
    public int OrderToSupplierId { get; set; }
    public int BatchId { get; set; }
    public int Quantity { get; set; }
    public bool Accepted { get; set; }

    // Constructor
    public OrderToSupplierBatch(int id, int orderToSupplierId, int batchId, int quantity, bool accepted)
    {
        Id = id;
        OrderToSupplierId = orderToSupplierId;
        BatchId = batchId;
        Quantity = quantity;
        Accepted = accepted;
    }
}