﻿using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Interfaces.REST.Resources;

namespace Restock.Platform.API.Resource.Interfaces.REST.Transform;

public static class OrderResourceFromEntityAssembler
{
    public static OrderResource ToResourceFromEntity(OrderToSupplier order)
    {
        return new OrderResource(
            order.OrderId, 
            order.Date, 
            order.EstimatedShipDate,
            order.EstimatedShipTime,
            order.Description, 
            order.AdminRestaurantId, 
            order.SupplierId,
            order.RequestedProductsCount, 
            order.TotalPrice,
            order.RequestedBatches
                .Select(b => new OrderToSupplierBatchResource(
                    b.OrderToSupplierId, 
                    b.BatchId,  
                    b.Quantity,
                    b.Accepted
                    ))
                .ToList());
    }
}
