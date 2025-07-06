using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Restock.Platform.API.Profiles.Domain.Model.Commands;
using Restock.Platform.API.Profiles.Domain.Model.Queries;
using Restock.Platform.API.Profiles.Domain.Services;
using Restock.Platform.API.Profiles.Interfaces.REST.Resources;
using Restock.Platform.API.Profiles.Interfaces.REST.Transform;
using Restock.Platform.API.Profiles.Domain.Model.Queries;
using Restock.Platform.API.Profiles.Domain.Services;
using Restock.Platform.API.Profiles.Interfaces.REST.Resources;
using Restock.Platform.API.Profiles.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Restock.Platform.API.Profiles.Interfaces.REST;
 
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Business Endpoints.")]
public class BusinessesController(
    IBusinessCommandService businessCommandService,
    IBusinessQueryService businessQueryService)
: ControllerBase
{
    [HttpGet("{businessId:int}")]
    [SwaggerOperation("Get Business by Id", "Get a Business by its unique identifier.", OperationId = "GetBusinessById")]
    [SwaggerResponse(200, "The Business was found and returned.", typeof(BusinessResource))]
    [SwaggerResponse(404, "The Business was not found.")]
    public async Task<IActionResult> GetBusinessById(int businessId)
    {
        var getBusinessByIdQuery = new GetBusinessByIdQuery(businessId);
        var business = await businessQueryService.Handle(getBusinessByIdQuery);
        if (business is null) return NotFound();
        var businessResource = BusinessResourceFromEntityAssembler.ToResourceFromEntity(business);
        return Ok(businessResource);
    }
    
    [HttpGet]
    [SwaggerOperation("Get All Businesses", "Get all Businesses.", OperationId = "GetAllBusinesses")]
    [SwaggerResponse(200, "The Businesses were found and returned.", typeof(IEnumerable<BusinessResource>))]
    [SwaggerResponse(404, "The Businesses were not found.")]
    public async Task<IActionResult> GetAllBusinesses()
    {
        var getAllBusinessesQuery = new GetAllBusinessesQuery();
        var businesses = await businessQueryService.Handle(getAllBusinessesQuery);
        var businessResources = businesses.Select(BusinessResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(businessResources);
    }

    // [HttpPost]
    // [SwaggerOperation("Create Business", "Create a new Business.", OperationId = "CreateBusiness")]
    // [SwaggerResponse(201, "The Business was created.", typeof(BusinessResource))]
    // [SwaggerResponse(400, "The Business was not created.")]
    // public async Task<IActionResult> CreateBusiness(CreateBusinessResource resource)
    // {
    //     var createBusinessCommand = CreateBusinessCommandFromResourceAssembler.ToCommandFromResource(resource);
    //     var business = await businessCommandService.Handle(createBusinessCommand);
    //     if (business is null) return BadRequest();
    //     var businessResource = BusinessResourceFromEntityAssembler.ToResourceFromEntity(business);
    //     return CreatedAtAction(nameof(GetBusinessById), new { BusinessId = business.Id }, businessResource);
    // }
     
    [HttpDelete("{businessId:int}")]
    [SwaggerOperation(
        Summary     = "Delete a Business",
        Description = "Deletes the Business with the given id.",
        OperationId = "DeleteBusiness")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Business deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Business not found")]
    public async Task<IActionResult> DeleteBusiness(int businessId)
    {
        await businessCommandService.Handle(new DeleteBusinessCommand(businessId));
        return NoContent();
    }
 
    [HttpPut("{businessId:int}")]
    [SwaggerOperation(
        Summary     = "Update an Business",
        Description = "Updates the Business's details.",
        OperationId = "UpdateBusiness")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Business updated")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Business not found")]
    public async Task<IActionResult> UpdateBusiness(
        int businessId,
        [FromBody] UpdateBusinessResource resource)
    {
        var cmd = UpdateBusinessCommandFromResourceAssembler
            .ToCommandFromResource(businessId, resource);
        await businessCommandService.Handle(cmd);
        return NoContent();
    }

   
}