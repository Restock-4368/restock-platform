﻿using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Domain.Model.Entities;

namespace Restock.Platform.API.Resource.Domain.Services;

public interface IOrderCommandService
{
    Task<OrderToSupplier?> Handle(CreateOrderCommand command);
    Task Handle(UpdateOrderCommand command);
    Task Handle(DeleteOrderCommand command);
    Task Handle(AddOrderRequestedBatchCommand command);
    Task Handle(UpdateOrderRequestedBatchCommand command); 
}