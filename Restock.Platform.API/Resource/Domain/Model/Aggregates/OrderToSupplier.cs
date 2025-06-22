using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Domain.Model.ValueObjects;

namespace Restock.Platform.API.Resource.Domain.Model.Entities;
/// <summary>
/// Represents a purchase order made to a supplier from a restaurant administration.
/// </summary>
public class OrderToSupplier
{
    /// <summary>
    /// Gets or sets the unique identifier of the order.
    /// </summary>
    public OrderIdentifier OrderId { get; set; }

    /// <summary>
    /// Gets or sets the creation date of the order.
    /// </summary>
    public DateTime Date { get; set; }

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

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderToSupplier"/> class.
    /// </summary>
    /// <param name="id">Order ID.</param>
    /// <param name="date">Order date.</param>
    /// <param name="estimatedShipDate">Estimated shipping date.</param>
    /// <param name="estimatedShipTime">Estimated shipping time.</param>
    /// <param name="description">Description of the order.</param>
    /// <param name="adminRestaurantId">Restaurant admin ID.</param>
    /// <param name="supplierId">Supplier ID.</param> 
    /// <param name="requestedProductsCount">Requested products count.</param>
    /// <param name="totalPrice">Total price of the order.</param> 
    public OrderToSupplier(DateTime date, DateTime? estimatedShipDate, DateTime? estimatedShipTime,
                           string? description, int adminRestaurantId, int supplierId,
                           int requestedProductsCount, decimal totalPrice)
    {
        OrderId = new OrderIdentifier(Guid.NewGuid());
        Date = date;
        EstimatedShipDate = estimatedShipDate;
        EstimatedShipTime = estimatedShipTime;
        Description = description;
        AdminRestaurantId = adminRestaurantId;
        SupplierId = supplierId; 
        State = EOrderToSupplierStates.OnHold;
        Situation = EOrderToSupplierSituations.Pending;
        RequestedProductsCount = requestedProductsCount;
        TotalPrice = totalPrice;
        PartiallyAccepted = false;
    }
   
    public OrderToSupplier(CreateOrderCommand command) : this(
        command.Date,
        command.EstimatedShipDate,
        command.EstimatedShipTime,
        command.Description,
        command.AdminRestaurantId,
        command.SupplierId,
        command.RequestedProductsCount,
        command.TotalPrice
    ){}
    
    
    private readonly List<OrderToSupplierBatch> _resquestBatches = new();
    public IReadOnlyCollection<OrderToSupplierBatch> Supplies => _resquestBatches.AsReadOnly();

    public void AddOrderToSupplierBatch(OrderIdentifier orderId, int batchId, double quantity, bool accepted)
    {
        var existing = _resquestBatches.FirstOrDefault(s => s.BatchId == batchId);

        if (existing != null) throw new InvalidOperationException("Batch already added to order");

        _resquestBatches.Add(new OrderToSupplierBatch(orderId, batchId, quantity, accepted));
    }
 
    
    public void Update(DateTime date,DateTime estimatedShipDate,
        DateTime estimatedShipTime,
        string description,
        int adminRestaurantId,
        int supplierId, 
        int requestedProductsCount,
        decimal totalPrice,
        bool partiallyAccepted)
    {
        Date = date;
        EstimatedShipDate = estimatedShipDate;
        EstimatedShipTime = estimatedShipTime;
        Description = description;
        AdminRestaurantId = adminRestaurantId;
        SupplierId = supplierId;  
        RequestedProductsCount = requestedProductsCount;
        TotalPrice = totalPrice;
        PartiallyAccepted = partiallyAccepted;
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
    
    public void ChangeToOnHold()
    {
        State = EOrderToSupplierStates.OnHold;
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
        State = EOrderToSupplierStates.Preparing;
    }
}
 