﻿using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Domain.Model.Aggregates;

namespace Restock.Platform.API.Resource.Domain.Services;

public interface IOrderCommandService
{
    Task<OrderToSupplier?> Handle(CreateOrderCommand command);
    Task Handle(UpdateOrderCommand command);
    Task Handle(DeleteOrderCommand command);
    Task Handle(AddOrderToSupplierBatchCommand command);
    Task Handle(UpdateOrderToSupplierBatchCommand command); 
    
    Task Handle(ApproveOrderToSupplierCommand command);
    Task Handle(DeclineOrderToSupplierCommand command);
    Task Handle(CancelOrderToSupplierCommand command);
    
    Task Handle(SetOrderPreparingCommand command);
    Task Handle(SetOrderOnTheWayCommand command);
    Task Handle(SetOrderDeliveredCommand command);
}