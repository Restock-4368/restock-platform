using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Domain.Repositories;
using Restock.Platform.API.Resource.Domain.Services;
using Restock.Platform.API.Shared.Domain.Repositories;

namespace Restock.Platform.API.Resource.Application.Internal.CommandServices;

public class OrderCommandService(IOrderRepository orderRepository, IUnitOfWork unitOfWork) : IOrderCommandService
{
    public async Task<OrderToSupplier?> Handle(CreateOrderCommand command)
    {
        var order = new OrderToSupplier(command);
        await orderRepository.AddAsync(order);
        await unitOfWork.CompleteAsync();
        return order; 
    }

    public async Task Handle(UpdateOrderCommand command)
    {
        var order = await orderRepository.FindByIdAsync(command.OrderId);

        if (order is null)
            throw new Exception("Order not found");

        order.Update(
            command.Date,
            command.EstimatedShipDate,
            command.EstimatedShipTime,
            command.Description,
            command.AdminRestaurantId,
            command.SupplierId,
            command.RequestedProductsCount,
            command.TotalPrice,
            command.PartiallyAccepted
        );

        orderRepository.Update(order);
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(DeleteOrderCommand command)
    {
        var order = await orderRepository.FindByIdAsync(command.OrderId);

        if (order is null)
            throw new Exception("Order not found");

        orderRepository.Remove(order);
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(AddOrderRequestedBatchCommand command)
    {
        var order = await orderRepository.FindByIdAsync(command.OrderId);

        if (order is null)
            throw new Exception("Order not found");

        order.AddOrderToSupplierBatch(
            command.OrderId,
            command.BatchId,
            command.Quantity,
            command.Accepted
        );

        orderRepository.Update(order);  
        await unitOfWork.CompleteAsync();
    }
    
    public async Task Handle(UpdateOrderToSupplierBatchCommand command)
    {
        var order = await orderRepository.FindByIdAsync(command.OrderId);

        if (order is null)
            throw new Exception("Order not found");

        var batch = order.RequestedBatches.FirstOrDefault(b => b.BatchId == command.BatchId);
        if (batch is null)
            throw new Exception("Batch not found in order");

        batch.Quantity = command.Quantity;
        batch.Accepted = command.Accepted;

        orderRepository.Update(order);
        await unitOfWork.CompleteAsync();
    }
}