namespace Restock.Platform.API.Resource.Domain.Model.Queries;

public record GetOrderToSupplierBatchesByOrderIdQuery(int OrderId, int OrderToSupplierBatchId);