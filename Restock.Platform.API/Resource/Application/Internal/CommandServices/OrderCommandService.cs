using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Domain.Model.ValueObjects;
using Restock.Platform.API.Resource.Domain.Repositories;
using Restock.Platform.API.Resource.Domain.Services;
using Restock.Platform.API.Shared.Domain.Exceptions; 
using Restock.Platform.API.Shared.Domain.Repositories;

namespace Restock.Platform.API.Resource.Application.Internal.CommandServices;

public class OrderCommandService(
    IOrderRepository orderRepository, 
    IBatchRepository batchRepository,
    ICustomSupplyRepository customSupplyRepository,
    ISupplyRepository supplyRepository,
    IUnitOfWork unitOfWork) : IOrderCommandService
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
            throw new BusinessRuleException("Order not found");

        if (order.State == EOrderToSupplierStates.Delivered)
            throw new BusinessRuleException("Cannot update an order that is delivered.");
        
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
            throw new BusinessRuleException("Order not found");
        
        if (order.Situation != EOrderToSupplierSituations.Declined && order.Situation != EOrderToSupplierSituations.Cancelled)
            throw new BusinessRuleException("Only orders with situation Declined or Cancelled can be deleted.");
        
        orderRepository.Remove(order);
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(AddOrderToSupplierBatchCommand command)
    {
        var order = await orderRepository.FindByIdAsync(command.OrderId);

        if (order is null)
            throw new BusinessRuleException("Order not found");

        if (order.State == EOrderToSupplierStates.Delivered
            || order.Situation == EOrderToSupplierSituations.Approved
            || order.Situation == EOrderToSupplierSituations.Declined
            || order.Situation == EOrderToSupplierSituations.Cancelled)
        {
            throw new BusinessRuleException("Cannot add requested batch to orders that are Delivered or Approved, Declined, or Cancelled.");
        }

        // Buscar el batch
        var batch = await batchRepository.FindByIdAsync(command.BatchId);
        if (batch is null)
            throw new BusinessRuleException("Batch not found.");

        if (batch.UserId != order.SupplierId)
            throw new BusinessRuleException("The batch does not belong to the supplier of this order.");
        
        // Buscar el CustomSupply del batch
        var customSupply = await customSupplyRepository.FindByIdAsync(batch.CustomSupplyId);
        if (customSupply is null)
            throw new BusinessRuleException("Custom supply not found.");

        // Validar que el admin restaurant tenga el supply
        var existsAdminCustomSupply = await customSupplyRepository.ExistsBySupplyIdAndUserIdAsync(customSupply.SupplyId, order.AdminRestaurantId);
        if (!existsAdminCustomSupply)
            throw new BusinessRuleException("Admin restaurant does not have this supply.");

        // Validar que el supplier tenga el supply
        var existsSupplierCustomSupply = await customSupplyRepository.ExistsBySupplyIdAndUserIdAsync(customSupply.SupplyId, order.SupplierId);
        if (!existsSupplierCustomSupply)
            throw new BusinessRuleException("Supplier does not have this supply.");

        // Agregar el batch
        order.AddOrderToSupplierBatch(
            command.OrderId,
            command.BatchId,
            command.Quantity
        );

        // Recalcular total price si corresponde
        var batches = await batchRepository.ListByIdsAsync(order.RequestedBatches.Select(rb => rb.BatchId).Distinct());
        order.RecalculateTotalPrice(batches);

        orderRepository.Update(order);
        await unitOfWork.CompleteAsync();
    }
    
    public async Task Handle(UpdateOrderToSupplierBatchCommand command)
    {
        var order = await orderRepository.FindByIdAsyncWithRequestedBatches(command.OrderId);

        if (order is null)
            throw new BusinessRuleException("Order not found");

        var batch = order.RequestedBatches.FirstOrDefault(b => b.BatchId == command.BatchId);
        if (batch is null)
            throw new BusinessRuleException("Batch not found in order");

        batch.Quantity = command.Quantity;

        orderRepository.Update(order);
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(ApproveOrderToSupplierCommand command)
    {
        var order = await orderRepository.FindByIdAsync(command.OrderId);
        if (order is null) throw new BusinessRuleException("Order not found");
        order.Approve();
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(DeclineOrderToSupplierCommand command)
    {
        var order = await orderRepository.FindByIdAsync(command.OrderId);
        if (order is null) throw new BusinessRuleException("Order not found");
        order.Decline();
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(CancelOrderToSupplierCommand command)
    {
        var order = await orderRepository.FindByIdAsync(command.OrderId);
        if (order is null) throw new BusinessRuleException("Order not found");
        order.Cancel();
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(SetOrderPreparingCommand command)
    {
        var order = await orderRepository.FindByIdAsync(command.OrderId);
        if (order is null) throw new BusinessRuleException("Order not found");
        order.ChangeToPreparing();
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(SetOrderOnTheWayCommand command)
    {
        var order = await orderRepository.FindByIdAsync(command.OrderId);
        if (order is null) throw new BusinessRuleException("Order not found");
        order.ChangeToOnTheWay();
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(SetOrderDeliveredCommand command)
    {
        var order = await orderRepository.FindByIdAsync(command.OrderId);
        if (order is null) throw new BusinessRuleException("Order not found");
        order.ChangeToDelivered();
        await unitOfWork.CompleteAsync();
    }
}