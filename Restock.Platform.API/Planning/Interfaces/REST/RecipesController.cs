using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Restock.Platform.API.Planning.Domain.Model.Commands;
using Restock.Platform.API.Planning.Domain.Services;
using Restock.Platform.API.Planning.Interfaces.REST.Resources;
using Restock.Platform.API.Planning.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Restock.Platform.API.Planning.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Recipe Endpoints")]
public class RecipesController(
    IRecipeCommandService recipeCommandService,
    IRecipeQueryService recipeQueryService) : ControllerBase
{
    [HttpGet("{recipeId:guid}")]
    [SwaggerOperation(
        Summary = "Get All Recipes",
        Description = "Returns a list of all available recipes.",
        OperationId = "GetAllRecipes")]
    [SwaggerResponse(StatusCodes.Status200OK, "Recipes found", typeof(RecipeResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No recipes found.")]
    public async Task<IActionResult> GetRecipeById([FromRoute] Guid recipeId)
    {
        var recipe = await recipeQueryService.GetByIdAsync(recipeId);
        if (recipe is null) return NotFound();

        var resource = RecipeResourceFromEntityAssembler.ToResource(recipe);
        return Ok(resource);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Recipes",
        Description = "Returns a list of all available recipes.",
        OperationId = "GetAllRecipes")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of recipes", typeof(IEnumerable<RecipeResource>))]
    public async Task<IActionResult> GetAllRecipes()
    {
        var recipes = await recipeQueryService.ListAsync();
        var resources = recipes.Select(RecipeResourceFromEntityAssembler.ToResource);
        return Ok(resources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a New Recipe",
        Description = "Creates a new recipe and returns the created recipe resource.",
        OperationId = "CreateRecipe")]
    [SwaggerResponse(StatusCodes.Status201Created, "Recipe created successfully", typeof(RecipeResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Recipe could not be created")]
    public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeResource resource)
    {
        var createRecipeCommand = CreateRecipeCommandFromResourceAssembler.ToCommand(resource);
        var recipeId = await recipeCommandService.Handle(createRecipeCommand);

        var recipe = await recipeQueryService.GetByIdAsync(recipeId);
        if (recipe is null) return BadRequest("Recipe could not be created.");

        var resourceCreated = RecipeResourceFromEntityAssembler.ToResource(recipe);
        return CreatedAtAction(nameof(GetRecipeById), new { recipeId = resourceCreated.Id }, resourceCreated);
    }

    [HttpPut("{recipeId:guid}")]
    [SwaggerOperation(
        Summary = "Update an existing Recipe",
        Description = "Updates a recipe by its ID.",
        OperationId = "UpdateRecipe")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Recipe updated successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Recipe not found")]
    public async Task<IActionResult> UpdateRecipe([FromRoute] Guid recipeId, [FromBody] CreateRecipeResource resource)
    {
        var updateRecipeCommand = new UpdateRecipeCommand(
            recipeId,
            resource.Name,
            resource.Description,
            resource.ImageUrl,
            resource.TotalPrice,
            resource.Supplies.Select(s => new SupplyInput(s.SupplyId, s.Quantity))
        );

        await recipeCommandService.Handle(updateRecipeCommand);
        return NoContent();
    }
    
    [HttpDelete("{recipeId:guid}")]
    [SwaggerOperation(
        Summary = "Delete Recipe by Id",
        Description = "Deletes a recipe by its ID.",
        OperationId = "DeleteRecipe")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Recipe deleted successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Recipe not found")]
    public async Task<IActionResult> DeleteRecipe([FromRoute] Guid recipeId)
    {
        await recipeCommandService.Handle(new DeleteRecipeCommand(recipeId));
        return NoContent();
    }
}