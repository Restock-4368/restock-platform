using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Restock.Platform.API.IAM.Application.Internal.QueryServices;
using Restock.Platform.API.IAM.Domain.Model.Queries;
using Restock.Platform.API.IAM.Domain.Services;
using Restock.Platform.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using Restock.Platform.API.IAM.Interfaces.REST.Resources;
using Restock.Platform.API.IAM.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Restock.Platform.API.IAM.Interfaces.REST;
 

/**
 * <summary>
 *     The Role's controller
 * </summary>
 * <remarks>
 *     This class is used to handle Role requests
 * </remarks>
 */
[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Role endpoints")]
public class RolesController(IRoleQueryService roleQueryService) : ControllerBase
{
    /**
     * <summary>
     *     Get Role by id endpoint. It allows to get a Role by id
     * </summary>
     * <param name="id">The Role id</param>
     * <returns>The Role resource</returns>
     */
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a Role by its id",
        Description = "Get a Role by its id",
        OperationId = "GetRoleById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The Role was found", typeof(RoleResource))]
    public async Task<IActionResult> GetRoleById(int id)
    {
        var getRoleByIdQuery = new GetRoleByIdQuery(id);
        var role = await roleQueryService.Handle(getRoleByIdQuery);
        var roleResource = RoleResourceFromEntityAssembler.ToResourceFromEntity(role!);
        return Ok(roleResource);
    }

    /**
     * <summary>
     *     Get all Roles' endpoint. It allows getting all Roles
     * </summary>
     * <returns>The Role resources</returns>
     */
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all Roles",
        Description = "Get all Roles",
        OperationId = "GetAllRoles")]
    [SwaggerResponse(StatusCodes.Status200OK, "The Roles were found", typeof(IEnumerable<RoleResource>))]
    public async Task<IActionResult> GetAllRoles()
    {
        var getAllRolesQuery = new GetAllRolesQuery();
        var roles = await roleQueryService.Handle(getAllRolesQuery);
        var roleResources = roles.Select(RoleResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(roleResources);
    }
}