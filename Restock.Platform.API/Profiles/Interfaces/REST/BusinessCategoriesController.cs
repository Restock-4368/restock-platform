using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Restock.Platform.API.Profiles.Domain.Model.Commands;
using Restock.Platform.API.Profiles.Domain.Model.Queries;
using Restock.Platform.API.Profiles.Domain.Services;
using Restock.Platform.API.Profiles.Interfaces.REST.Resources;
using Restock.Platform.API.Profiles.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Restock.Platform.API.Profiles.Interfaces.REST;
 
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Business Categories Endpoints.")]
public class BusinessCategoriesController( 
    IBusinessCategoryQueryService businessQueryService)
: ControllerBase
{ 
    [HttpGet]
    [SwaggerOperation("Get All Businesses", "Get all Business Categories.", OperationId = "GetAllBusinessCategories")]
    [SwaggerResponse(200, "The Business Categories were found and returned.", typeof(IEnumerable<BusinessCategoryResource>))]
    [SwaggerResponse(404, "The Business Categories were not found.")]
    public async Task<IActionResult> GetAllBusinessCategories()
    {
        var getAllBusinessCategoriesQuery = new GetAllBusinessCategoriesQuery();
        var businessCategories = await businessQueryService.Handle(getAllBusinessCategoriesQuery);
        var businessCategoryResources = businessCategories.Select(BusinessCategoryResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(businessCategoryResources);
    } 
   
}