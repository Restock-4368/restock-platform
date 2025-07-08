using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Domain.Model.Entities;
using Restock.Platform.API.Resource.Domain.Model.ValueObjects;
using Restock.Platform.API.Shared.Domain.Exceptions;

namespace Restock.Platform.API.Resource.Domain.Model.Aggregates;
/// <summary>
/// Represents a purchase order made to a supplier from a restaurant administration.
/// </summary>
public partial class OrderToSupplier
{
    /// <summary>
    /// Gets or sets the unique identifier of the order.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the estimated shipping date.
    /// </summary>
    public DateTime? EstimatedShipDate { get; set; }

    /// <summary>
    /// Gets or sets the estimated time the order will be shipped.
    /// </summary>
    public DateTime? EstimatedShipTime { get; set; }

    /// <summary>
    /// Gets or sets a textual description of the order.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the ID of the restaurant admin placing the order.
    /// </summary>
    public int AdminRestaurantId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the supplier to whom the order is made.
    /// </summary>
    public int SupplierId { get; set; }

    /// <summary>
    /// Gets or sets the current state ID of the order (e.g., pending, approved).
    /// </summary>
    public EOrderToSupplierStates State { get; set; } = EOrderToSupplierStates.OnHold;

    /// <summary>
    /// Gets or sets the situation ID of the order (e.g., new, delayed, rejected).
    /// </summary>
    public EOrderToSupplierSituations Situation { get; set; } = EOrderToSupplierSituations.Pending;

    /// <summary>
    /// Gets or sets the number of products requested in the order.
    /// </summary>
    public int RequestedProductsCount { get; set; }

    /// <summary>
    /// Gets or sets the total price of the order.
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the order was partially accepted.
    /// </summary>
    public bool PartiallyAccepted { get; set; } = false;
    
    private readonly List<OrderToSupplierBatch> _requestedBatches = new();
    public IReadOnlyCollection<OrderToSupplierBatch> RequestedBatches => _requestedBatches.AsReadOnly();

    
    
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderToSupplier"/> class.
    /// </summary>
    /// <param name="id">Order ID.</param>  
    /// <param name="description">Description of the order.</param>
    /// <param name="adminRestaurantId">Restaurant admin ID.</param>
    /// <param name="supplierId">Supplier ID.</param>  
    public OrderToSupplier(string? description, int adminRestaurantId, int supplierId)
    {
        if (adminRestaurantId <= 0)
            throw new BusinessRuleException("AdminRestaurantId must be greater than zero.");
        if (supplierId <= 0)
            throw new BusinessRuleException("SupplierId must be greater than zero.");
        if (adminRestaurantId == supplierId)
            throw new BusinessRuleException("AdminRestaurantId and SupplierId cannot be the same.");
        if (string.IsNullOrWhiteSpace(description))
            throw new BusinessRuleException("Description cannot be empty.");
        
        EstimatedShipDate = null;
        EstimatedShipTime = null;
        Description = description;
        AdminRestaurantId = adminRestaurantId;
        SupplierId = supplierId; 
        State = EOrderToSupplierStates.OnHold;
        Situation = EOrderToSupplierSituations.Pending;
        RequestedProductsCount = _requestedBatches.Count;
        TotalPrice = 0;
        PartiallyAccepted = false;
    }
   
    public OrderToSupplier(CreateOrderCommand command) : this(
        command.Description,
        command.AdminRestaurantId,
        command.SupplierId
    ){}
    
    public void AddOrderToSupplierBatch(int orderId, int batchId, double quantity)
    {
        var existing = _requestedBatches.FirstOrDefault(s => s.BatchId == batchId);

        if (existing != null) throw new InvalidOperationException("Batch already added to order");

        _requestedBatches.Add(new OrderToSupplierBatch(orderId, batchId, quantity));
    }
 
    
    public void Update(DateTime? estimatedShipDate,
        DateTime? estimatedShipTime,
        string? description)
    { 
        EstimatedShipDate = estimatedShipDate;
        EstimatedShipTime = estimatedShipTime;
        Description = description;
    }
     
    public void Approve()
    {
        Situation = EOrderToSupplierSituations.Approved;
    }
    
    public void Decline()
    {
        Situation = EOrderToSupplierSituations.Declined;
    }
    
    public void Cancel()
    {
        Situation = EOrderToSupplierSituations.Cancelled;
    }
     
    public void ChangeToPreparing()
    {
        State = EOrderToSupplierStates.Preparing;
    }
    
    public void ChangeToOnTheWay()
    {
        State = EOrderToSupplierStates.OnTheWay;
    }
    
    public void ChangeToDelivered()
    {
        State = EOrderToSupplierStates.Delivered;
    }
    
    public void RecalculateTotalPrice(IEnumerable<Batch> batches)
    {
        decimal total = 0;

        foreach (var requestedBatch in _requestedBatches)
        { 
            var batch = batches.FirstOrDefault(b => b.Id == requestedBatch.BatchId);
            if (batch != null && batch.CustomSupply != null)
            {
                 total += (decimal)requestedBatch.Quantity * batch.CustomSupply.Price;
            }
        }

        TotalPrice = total;
    }
    
}
 