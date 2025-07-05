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
[SwaggerTag("Available Batches endpoints")]
public class BatchesController(IBatchCommandService batchCommandService,
    IBatchQueryService batchQueryService) : ControllerBase
{
    [HttpGet("{batchId:int}")]
    [SwaggerOperation(
        Summary = "Get Batch by Id",
        Description = "Returns a Batch by its unique identifier.",
        OperationId = "GetBatchById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Batch found", typeof(BatchResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Batch not found")]
    public async Task<IActionResult> GetBatchById(int batchId)
    {
        var getBatchByIdQuery = new GetBatchByIdQuery(batchId);
        var batch = await batchQueryService.Handle(getBatchByIdQuery);
        
        if (batch is null) {  return NotFound(); }

        var batchResource = BatchResourceFromEntityAssembler.ToResourceFromEntity(batch);
        
        return Ok(batchResource); 
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Batches to supplier",
        Description = "Returns a list of all available batches.",
        OperationId = "GetAllBatches")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No batches found")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of batches", typeof(IEnumerable<BatchResource>))]
    public async Task<IActionResult> GetAllBatches()
    {
        var batches = await batchQueryService.Handle(new GetAllBatchesQuery());
        var batchesResources = batches
            .Select(BatchResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(batchesResources);
    }
    
    //Commands
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a New Batch",
        Description = "Creates a new Batch and returns the created Batch resource.",
        OperationId = "CreateBatch")]
    [SwaggerResponse(StatusCodes.Status201Created, "Batch created successfully", typeof(BatchResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Batch could not be created")]
    public async Task<IActionResult> CreateBatch([FromBody] CreateBatchResource resource)
    {
        var createBatchCommand = CreateBatchCommandFromResourceAssembler.ToCommandFromResource(resource); 
        var batch = await batchCommandService.Handle(createBatchCommand);
        if (batch is null) return BadRequest("Batch could not be created.");
        var batchResource = BatchResourceFromEntityAssembler.ToResourceFromEntity(batch);
        return CreatedAtAction(nameof(GetBatchById), new { batchId  = batchResource.Id }, batchResource); 
    }
    
    // DELETE /api/v1/batches/{batchId}
    [HttpDelete("{batchId:int}")]
    [SwaggerOperation(
        Summary     = "Delete a Batch",
        Description = "Deletes the batch with the given id.",
        OperationId = "DeleteBatch")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Batch deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Batch not found")]
    public async Task<IActionResult> DeleteBatch(int batchId)
    {
        await batchCommandService.Handle(new DeleteBatchCommand(batchId));
        return NoContent();
    }

    // PUT /api/v1/batches/{batchId}
    [HttpPut("{batchId:int}")]
    [SwaggerOperation(
        Summary     = "Update an Batch",
        Description = "Updates the Batch's details.",
        OperationId = "UpdateBatch")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Batch updated")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Batch not found")]
    public async Task<IActionResult> UpdateBatch(
        int batchId,
        [FromBody] UpdateBatchResource resource)
    {
        var cmd = UpdateBatchCommandFromResourceAssembler
            .ToCommandFromResource(batchId, resource);
        await batchCommandService.Handle(cmd);
        return NoContent();
    }

}