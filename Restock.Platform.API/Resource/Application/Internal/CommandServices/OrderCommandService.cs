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
            command.EstimatedShipDate,
            command.EstimatedShipTime,
            command.Description
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

    public async Task Handle(AddOrderToSupplierBatchCommand command)
    {
        var order = await orderRepository.FindByIdAsync(command.OrderId);

        if (order is null)
            throw new Exception("Order not found");

        order.AddOrderToSupplierBatch(
            command.OrderId,
            command.BatchId,
            command.Quantity
        );

        orderRepository.Update(order);  
        await unitOfWork.CompleteAsync();
    }
    
    public async Task Handle(UpdateOrderToSupplierBatchCommand command)
    {
        var order = await orderRepository.FindByIdAsyncWithRequestedBatches(command.OrderId);

        if (order is null)
            throw new Exception("Order not found");

        var batch = order.RequestedBatches.FirstOrDefault(b => b.BatchId == command.BatchId);
        if (batch is null)
            throw new Exception("Batch not found in order");

        batch.Quantity = command.Quantity;

        orderRepository.Update(order);
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(ApproveOrderToSupplierCommand command)
    {
        var order = await orderRepository.FindByIdAsync(command.OrderId);
        if (order is null) throw new Exception("Order not found");
        order.Approve();
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(DeclineOrderToSupplierCommand command)
    {
        var order = await orderRepository.FindByIdAsync(command.OrderId);
        if (order is null) throw new Exception("Order not found");
        order.Decline();
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(CancelOrderToSupplierCommand command)
    {
        var order = await orderRepository.FindByIdAsync(command.OrderId);
        if (order is null) throw new Exception("Order not found");
        order.Cancel();
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(SetOrderPreparingCommand command)
    {
        var order = await orderRepository.FindByIdAsync(command.OrderId);
        if (order is null) throw new Exception("Order not found");
        order.ChangeToPreparing();
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(SetOrderOnTheWayCommand command)
    {
        var order = await orderRepository.FindByIdAsync(command.OrderId);
        if (order is null) throw new Exception("Order not found");
        order.ChangeToOnTheWay();
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(SetOrderDeliveredCommand command)
    {
        var order = await orderRepository.FindByIdAsync(command.OrderId);
        if (order is null) throw new Exception("Order not found");
        order.ChangeToDelivered();
        await unitOfWork.CompleteAsync();
    }
}