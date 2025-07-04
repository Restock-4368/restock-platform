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
[SwaggerTag("Available Supplies endpoints")]
public class SuppliesController(ISupplyQueryService supplyQueryService) : ControllerBase
{
    [HttpGet("{supplyId:int}")]
    [SwaggerOperation(
        Summary = "Get Supply by Id",
        Description = "Returns a Supply by its unique identifier.",
        OperationId = "GetSupplyById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Supply found", typeof(SupplyResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Supply not found")]
    public async Task<IActionResult> GetSupplyById(int supplyId)
    {
        var getSupplyByIdQuery = new GetSupplyByIdQuery(supplyId);
        var supply = await supplyQueryService.Handle(getSupplyByIdQuery);
        
        if (supply is null) {  return NotFound(); }

        var supplyResource = SupplyResourceFromEntityAssembler.ToResourceFromEntity(supply);
        
        return Ok(supplyResource); 
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All supplies",
        Description = "Returns a list of all available supplIes.",
        OperationId = "GetAllSupplies")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No supplies found")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of supplies", typeof(IEnumerable<SupplyResource>))]
    public async Task<IActionResult> GetAllSupplies()
    {
        var supplies = await supplyQueryService.Handle(new GetAllSuppliesQuery());
        var suppliesResources = supplies
            .Select(SupplyResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(suppliesResources);
    }
}