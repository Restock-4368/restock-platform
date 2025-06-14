namespace Restock.Platform.API.Resource.Domain.Model.Entities;
/// <summary>
/// Represents a purchase order made to a supplier from a restaurant administration.
/// </summary>
public class OrderToSupplier
{
    /// <summary>
    /// Gets or sets the unique identifier of the order.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the creation date of the order.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the estimated shipping date.
    /// </summary>
    public DateTime EstimatedShipDate { get; set; }

    /// <summary>
    /// Gets or sets the estimated time the order will be shipped.
    /// </summary>
    public DateTime EstimatedShipTime { get; set; }

    /// <summary>
    /// Gets or sets a textual description of the order.
    /// </summary>
    public string Description { get; set; }

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
    public int OrderToSupplierStateId { get; set; }

    /// <summary>
    /// Gets or sets the situation ID of the order (e.g., new, delayed, rejected).
    /// </summary>
    public int OrderToSupplierSituationId { get; set; }

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
    public bool PartiallyAccepted { get; set; }

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
    /// <param name="orderToSupplierStateId">Order state ID.</param>
    /// <param name="orderToSupplierSituationId">Order situation ID.</param>
    /// <param name="requestedProductsCount">Requested products count.</param>
    /// <param name="totalPrice">Total price of the order.</param>
    /// <param name="partiallyAccepted">Whether the order was partially accepted.</param>
    public OrderToSupplier(int id, DateTime date, DateTime estimatedShipDate, DateTime estimatedShipTime,
                           string description, int adminRestaurantId, int supplierId, int orderToSupplierStateId,
                           int orderToSupplierSituationId, int requestedProductsCount, decimal totalPrice, bool partiallyAccepted)
    {
        Id = id;
        Date = date;
        EstimatedShipDate = estimatedShipDate;
        EstimatedShipTime = estimatedShipTime;
        Description = description;
        AdminRestaurantId = adminRestaurantId;
        SupplierId = supplierId;
        OrderToSupplierStateId = orderToSupplierStateId;
        OrderToSupplierSituationId = orderToSupplierSituationId;
        RequestedProductsCount = requestedProductsCount;
        TotalPrice = totalPrice;
        PartiallyAccepted = partiallyAccepted;
    }
}
