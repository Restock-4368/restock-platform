using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
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
        var batches = await orderQueryService.Handle(new GetOrderBatchesByOrderIdQuery(orderId));
        var batchResources = batches.Select(b => new BatchResource(b.BatchId, b.CustomSupplyId, b.CustomSupply, b.Stock, b.ExpirationDate, b.UserId)); 
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
        var customSupplies = await orderQueryService.Handle(new GetOrderCustomSuppliesQuery(orderId));
        
        if (customSupplies is null)
            return NotFound();
        
        var customSupplyResources = customSupplies
            .Select(CustomSupplyResourceAssembler.ToResourceFromEntity);
        
        return Ok(customSupplyResources); 
    }
    
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
        return CreatedAtAction(nameof(GetOrderToSupplierById), new { categoryId = orderResource.OrderId }, orderResource);
    }
    
  
}