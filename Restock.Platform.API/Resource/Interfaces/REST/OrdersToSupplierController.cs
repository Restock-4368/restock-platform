using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Domain.Model.Queries;
using Restock.Platform.API.Resource.Domain.Services;
using Restock.Platform.API.Resource.Interfaces.REST.Resources;
using Restock.Platform.API.Resource.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Restock.Platform.API.Resource.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Orders to Supplier endpoints")]
public class OrdersToSupplierController( 
    IOrderCommandService orderCommandService,
    IOrderQueryService orderQueryService) : ControllerBase
{
    [HttpGet("{orderId:int}")]
    [SwaggerOperation(
        Summary = "Get Order to supplier by Id",
        Description = "Returns a order to supplier by its unique identifier.",
        OperationId = "GetOrderToSupplierById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Order to supplier found", typeof(OrderResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Order to supplier not found")]
    public async Task<IActionResult> GetOrderToSupplierById(int orderId)
    {
        var getOrderToSupplierByIdQuery = new GetOrderByIdQuery(orderId);
        var order = await orderQueryService.Handle(getOrderToSupplierByIdQuery);
        
        if (order is null) {  return NotFound(); }

        var orderResource = OrderResourceFromEntityAssembler.ToResourceFromEntity(order);
        
        return Ok(orderResource); 
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Orders to supplier",
        Description = "Returns a list of all available orders to supplier.",
        OperationId = "GetAllOrders")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of orders", typeof(IEnumerable<OrderResource>))]
    public async Task<IActionResult> GetAllOrdersToSupplier()
    {
        var orders = await orderQueryService.Handle(new GetAllOrdersQuery());
        var orderResources = orders
            .Select(OrderResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(orderResources);
    }
    
    [HttpGet("supplier/{supplierId:int}")]
    [SwaggerOperation(
        Summary = "Get All Orders to supplier by Supplier Id",
        Description = "Returns all orders to supplier made to a specific supplier.",
        OperationId = "GetOrdersBySupplierId")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of orders", typeof(IEnumerable<OrderResource>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No orders found for the given supplier")]
    public async Task<IActionResult> GetOrdersBySupplierId(int supplierId)
    { 
        var orders = await orderQueryService.Handle(new GetAllOrdersBySupplierIdQuery(supplierId));
         
        var orderResources = orders.Select(OrderResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(orderResources);
    }
    
    [HttpGet("orders/{orderId:int}/batches")]
    [SwaggerOperation(
        Summary = "Get Batches for Order",
        Description = "Returns the batches associated with an order.",
        OperationId = "GetOrderBatches")]
    [SwaggerResponse(StatusCodes.Status200OK, "Batches found", typeof(IEnumerable<BatchResource>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Order not found")]
    public async Task<IActionResult> GetBatchesForOrder(int orderId)
    { 
        var batches = (await orderQueryService
                .Handle(new GetOrderBatchesByOrderIdQuery(orderId)))
            .ToList();    

        if (!batches.Any())
            return NotFound();

        var batchResources = batches
            .Select(BatchResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(batchResources); 
        
    }
    
    [HttpGet("{orderId:int}/supplies")]
    [SwaggerOperation(
        Summary = "Get Supplies for Order",
        Description = "Returns the supplies related to an order's batches.",
        OperationId = "GetOrderSupplies")]
    [SwaggerResponse(StatusCodes.Status200OK, "Supplies found", typeof(IEnumerable<SupplyResource>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Order not found or supplies unavailable")]
    public async Task<IActionResult> GetCustomSuppliesForOrder(int orderId)
    { 
        var customSupplies = (await orderQueryService
                .Handle(new GetOrderCustomSuppliesByOrderIdQuery(orderId)))
            .ToList();    

        if (!customSupplies.Any())
            return NotFound();

        var customSupplyResources = customSupplies
            .Select(CustomSupplyResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(customSupplyResources); 
    }
    
    [HttpGet("{orderId:int}/requested-batches")]
    [SwaggerOperation(
        Summary = "Get Requested Batches for Order",
        Description = "Returns the batches requested related to an order.",
        OperationId = "GetRequestedBatchesForOrder")]
    [SwaggerResponse(StatusCodes.Status200OK, "Requested Batches found", typeof(IEnumerable<SupplyResource>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Order not found or Requested Batches unavailable")]
    public async Task<IActionResult> GetOrderToSupplierBatchesForOrder(int orderId)
    {
        var requestedBatches = (await orderQueryService
                .Handle(new GetOrderToSupplierBatchesByOrderIdQuery(orderId)))
            .ToList();    

        if (!requestedBatches.Any())
            return NotFound();

        var requestedBatchResources = requestedBatches
            .Select(OrderToSupplierBatchResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(requestedBatchResources); 
    }
    
    //Commands 
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a New Order to Supplier",
        Description = "Creates a new order to Supplier and returns the created order to supplier resource.",
        OperationId = "CreateOrder")]
    [SwaggerResponse(StatusCodes.Status201Created, "Order to supplier created successfully", typeof(OrderResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Order to supplier could not be created")]
    public async Task<IActionResult> CreateOrderToSupplier([FromBody] CreateOrderResource resource)
    {
        var createOrderCommand = CreateOrderCommandFromResourceAssembler.ToCommandFromResource(resource);
        var order = await orderCommandService.Handle(createOrderCommand);
        if (order is null) return BadRequest("Order to supplier could not be created.");
        var orderResource = OrderResourceFromEntityAssembler.ToResourceFromEntity(order);
        return CreatedAtAction(nameof(GetOrderToSupplierById), new { categoryId = orderResource.Id }, orderResource);
    }
    
     // DELETE /api/v1/orders/{orderId}
    [HttpDelete("{orderId:int}")]
    [SwaggerOperation(
        Summary     = "Delete an Order to Supplier",
        Description = "Deletes the order with the given id.",
        OperationId = "DeleteOrderToSupplier")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Order deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Order not found")]
    public async Task<IActionResult> DeleteOrderToSupplier(int orderId)
    {
        await orderCommandService.Handle(new DeleteOrderCommand(orderId));
        return NoContent();
    }

    // PUT /api/v1/orders/{orderId}
    [HttpPut("{orderId:int}")]
    [SwaggerOperation(
        Summary     = "Update an Order to Supplier",
        Description = "Updates the order's details.",
        OperationId = "UpdateOrderToSupplier")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Order updated")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Order not found")]
    public async Task<IActionResult> UpdateOrderToSupplier(
        int orderId,
        [FromBody] UpdateOrderResource resource)
    {
        var cmd = UpdateOrderCommandFromResourceAssembler
            .ToCommandFromResource(orderId, resource);
        await orderCommandService.Handle(cmd);
        return NoContent();
    }

    // PUT /api/v1/orders/{orderId}/requested-batches
    [HttpPut("{orderId:int}/requested-batches")]
    [SwaggerOperation(
        Summary     = "Update Requested Batches for Order",
        Description = "Updates the quantities or acceptance flags of batches for this order.",
        OperationId = "UpdateOrderRequestedBatches")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Requested batches updated")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Order or batch not found")]
    public async Task<IActionResult> UpdateOrderRequestedBatches(
        int orderId,
        [FromBody] UpdateOrderToSupplierBatchResource resource)
    {
        var cmd = UpdateOrderToSupplierBatchCommandFromResourceAssembler
            .ToCommandFromResource(orderId, resource);
        await orderCommandService.Handle(cmd);
        return NoContent();
    }
  
    [HttpPost("{orderId:int}/requested-batches")]
    [SwaggerOperation(
        Summary     = "Add requested batches to an order",
        Description = "Adds multiple requested batches to an order.",
        OperationId = "AddRequestedBatchesToOrder")]
    [SwaggerResponse(StatusCodes.Status201Created, "Requested batches added successfully")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Requested batches added successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Order not found")]
    public async Task<IActionResult> AddRequestedBatchesToOrder(
        [FromRoute] int orderId,
        [FromBody] List<AddOrderToSupplierBatchResource> orderToSupplierBatches)
    { 
        var existing = await orderQueryService.Handle(new GetOrderByIdQuery(orderId));
        if (existing is null)
            return NotFound();
 
        foreach (var orderToSupplierBatch in orderToSupplierBatches)
        {
            var addOrderToSupplierBatchCommand = AddOrderToSupplierBatchFromResourceAssembler
                .ToCommandFromResource(orderToSupplierBatch);
            await orderCommandService.Handle(addOrderToSupplierBatchCommand);
        }
 
        return CreatedAtAction(
            nameof(GetOrderToSupplierById),
            new { orderId },
            null
        );
    }
    
    //Commands to change situation and state of the order to supplier
    
    // POST /api/v1/orders/{orderId}/approve
    [HttpPost("{orderId:int}/approve")]
    [SwaggerOperation(Summary = "Approve an order", OperationId = "ApproveOrderToSupplier")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Order approved")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Order not found")]
    public async Task<IActionResult> Approve(int orderId)
    {
        await orderCommandService.Handle(new ApproveOrderToSupplierCommand(orderId));
        return NoContent();
    }

    // POST /api/v1/orders/{orderId}/decline
    [HttpPost("{orderId:int}/decline")]
    [SwaggerOperation(Summary = "Decline an order", OperationId = "DeclineOrderToSupplier")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Order declined")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Order not found")]
    public async Task<IActionResult> Decline(int orderId)
    {
        await orderCommandService.Handle(new DeclineOrderToSupplierCommand(orderId));
        return NoContent();
    }

    // POST /api/v1/orders/{orderId}/cancel
    [HttpPost("{orderId:int}/cancel")]
    [SwaggerOperation(Summary = "Cancel an order", OperationId = "CancelOrderToSupplier")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Order cancelled")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Order not found")]
    public async Task<IActionResult> Cancel(int orderId)
    {
        await orderCommandService.Handle(new CancelOrderToSupplierCommand(orderId));
        return NoContent();
    }

    // POST /api/v1/orders/{orderId}/preparing
    [HttpPost("{orderId:int}/preparing")]
    [SwaggerOperation(Summary = "Mark order as Preparing", OperationId = "SetOrderPreparing")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Order marked as Preparing")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Order not found")]
    public async Task<IActionResult> SetPreparing(int orderId)
    {
        await orderCommandService.Handle(new SetOrderPreparingCommand(orderId));
        return NoContent();
    }

    // POST /api/v1/orders/{orderId}/on-the-way
    [HttpPost("{orderId:int}/on-the-way")]
    [SwaggerOperation(Summary = "Mark order as On The Way", OperationId = "SetOrderOnTheWay")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Order marked as On The Way")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Order not found")]
    public async Task<IActionResult> SetOnTheWay(int orderId)
    {
        await orderCommandService.Handle(new SetOrderOnTheWayCommand(orderId));
        return NoContent();
    }

    // POST /api/v1/orders/{orderId}/delivered
    [HttpPost("{orderId:int}/delivered")]
    [SwaggerOperation(Summary = "Mark order as Delivered", OperationId = "SetOrderDelivered")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Order marked as Delivered")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Order not found")]
    public async Task<IActionResult> SetDelivered(int orderId)
    {
        await orderCommandService.Handle(new SetOrderDeliveredCommand(orderId));
        return NoContent();
    }
}