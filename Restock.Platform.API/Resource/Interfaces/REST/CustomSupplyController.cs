using System.Net.Mime;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Restock.Platform.API.Resource.Application.Internal.CommandServices;
using Restock.Platform.API.Resource.Application.Internal.QueryServices;
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
[SwaggerTag("Available Custom Supplies endpoints")]
public class CustomSupplyController(ICustomSupplyCommandService customSupplyCommandService,
    ICustomSupplyQueryService customSupplyQueryService) : ControllerBase
{
    [HttpGet("{CustomSupplyId:int}")]
    [SwaggerOperation(
        Summary = "Get CustomSupply by Id",
        Description = "Returns a CustomSupply by its unique identifier.",
        OperationId = "GetCustomSupplyById")]
    [SwaggerResponse(StatusCodes.Status200OK, "CustomSupply found", typeof(CustomSupplyResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "CustomSupply not found")]
    public async Task<IActionResult> GetCustomSupplyById(int customSupplyId)
    {
        var getCustomSupplyByIdQuery = new GetCustomSupplyByIdQuery(customSupplyId);
        var customSupply = await customSupplyQueryService.Handle(getCustomSupplyByIdQuery);
        
        if (customSupply is null) {  return NotFound(); }

        var customSupplyResource = CustomSupplyResourceFromEntityAssembler.ToResourceFromEntity(customSupply);
        
        return Ok(customSupplyResource); 
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Custom supplIes",
        Description = "Returns a list of all available Custom supplIes.",
        OperationId = "GetAllCustomSupplies")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of CustomSupplies", typeof(IEnumerable<CustomSupplyResource>))]
    public async Task<IActionResult> GetAllCustomSupplies()
    {
        var customSupplies = await customSupplyQueryService.Handle(new GetAllCustomSuppliesQuery());
        var customSuppliesResources = customSupplies
            .Select(CustomSupplyResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(customSuppliesResources);
    }
    
    //Commands
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a New CustomSupply",
        Description = "Creates a new CustomSupply and returns the created CustomSupply resource.",
        OperationId = "CreateCustomSupply")]
    [SwaggerResponse(StatusCodes.Status201Created, "CustomSupply created successfully", typeof(CustomSupplyResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "CustomSupply could not be created")]
    public async Task<IActionResult> CreateCustomSupply([FromBody] CreateCustomSupplyResource resource)
    {
        var createCustomSupplyCommand = CreateCustomSupplyCommandFromResourceAssembler.ToCommandFromResource(resource); 
        var customSupply = await customSupplyCommandService.Handle(createCustomSupplyCommand);
        if (customSupply is null) return BadRequest("CustomSupply could not be created.");
        var customSupplyResource = CustomSupplyResourceFromEntityAssembler.ToResourceFromEntity(customSupply);
        return CreatedAtAction(nameof(GetCustomSupplyById), new { CustomSupplyId  = customSupplyResource.Id }, customSupplyResource); 
    }
    
    // DELETE /api/v1/CustomSupplyes/{CustomSupplyId}
    [HttpDelete("{CustomSupplyId:int}")]
    [SwaggerOperation(
        Summary     = "Delete a CustomSupply",
        Description = "Deletes the CustomSupply with the given id.",
        OperationId = "DeleteCustomSupply")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "CustomSupply deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "CustomSupply not found")]
    public async Task<IActionResult> DeleteCustomSupply(int customSupplyId)
    {
        await customSupplyCommandService.Handle(new DeleteCustomSupplyCommand(customSupplyId));
        return NoContent();
    }

    // PUT /api/v1/CustomSupplies/{CustomSupplyId}
    [HttpPut("{customSupplyId:int}")]
    [SwaggerOperation(
        Summary     = "Update an CustomSupply",
        Description = "Updates the CustomSupply's details.",
        OperationId = "UpdateCustomSupply")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "CustomSupply updated")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "CustomSupply not found")]
    public async Task<IActionResult> UpdateCustomSupply(
        int customSupplyId,
        [FromBody] UpdateCustomSupplyResource resource)
    {
        var cmd = UpdateCustomSupplyCommandFromResourceAssembler
            .ToCommandFromResource(customSupplyId, resource);
        await customSupplyCommandService.Handle(cmd);
        return NoContent();
    }
}