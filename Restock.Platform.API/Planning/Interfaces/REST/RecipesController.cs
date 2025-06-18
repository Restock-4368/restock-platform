using System;
using System.Net.Mime;
using System.Linq;
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
        Summary = "Get Recipe by Id",
        Description = "Returns a recipe.",
        OperationId = "GetAllRecipes")]
    [SwaggerResponse(StatusCodes.Status200OK, "Recipes found", typeof(RecipeResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No recipes found.")]
    public async Task<IActionResult> GetRecipeById([FromRoute] Guid recipeId, [FromQuery] string? include)
    {
        var withSupplies = string.Equals(include, "supplies", StringComparison.OrdinalIgnoreCase);
        var recipe = await recipeQueryService.GetByIdAsync(recipeId, withSupplies);
        if (recipe is null) return NotFound();

        var resource = RecipeResourceFromEntityAssembler.ToResource(recipe, withSupplies);
        return Ok(resource);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Recipes",
        Description = "Returns a list of all available recipes.",
        OperationId = "GetAllRecipes")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of recipes", typeof(IEnumerable<RecipeResource>))]
    public async Task<IActionResult> GetAllRecipes([FromQuery] string? include)
    {
        var withSupplies = string.Equals(include, "supplies", StringComparison.OrdinalIgnoreCase);
        var recipes = await recipeQueryService.ListAsync(withSupplies);
        var resources = recipes.Select(r => RecipeResourceFromEntityAssembler.ToResource(r, withSupplies));
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
            resource.TotalPrice
        );

        await recipeCommandService.Handle(updateRecipeCommand);
        return NoContent();
    }

    [HttpPost("{recipeId:guid}/supplies")]
    [SwaggerOperation(
        Summary = "Add Supply to Recipe",
        Description = "Adds a supply to a recipe.",
        OperationId = "AddSupplyToRecipe")]
    [SwaggerResponse(StatusCodes.Status201Created, "Supply added successfully")]
    public async Task<IActionResult> AddSupplyToRecipe([FromRoute] Guid recipeId,
        [FromBody] AddRecipeSupplyResource resource)
    {
        var command = new AddRecipeSupplyCommand(recipeId, resource.SupplyId, resource.Quantity);
        await recipeCommandService.Handle(command);
        return Ok();
    }

    [HttpGet("{recipeId:guid}/supplies")]
    public async Task<IActionResult> GetRecipeSupplies([FromRoute] Guid recipeId)
    {
        var supplies = await recipeQueryService.ListSuppliesByRecipeIdAsync(recipeId);
        var resources = supplies.Select(s => new RecipeSupplyResource(s.SupplyId.Value, s.Quantity.Value)).ToList();
        return Ok(resources);
    }

    [HttpPut("{recipeId:guid}/supplies/{supplyId:guid}")]
    public async Task<IActionResult> UpdateRecipeSupply([FromRoute] Guid recipeId, [FromRoute] Guid supplyId, [FromBody] UpdateRecipeSupplyResource resource)
    {
        await recipeCommandService.Handle(new UpdateRecipeSupplyCommand(recipeId, supplyId, resource.Quantity));
        return NoContent();
    }

    [HttpDelete("{recipeId:guid}/supplies/{supplyId:guid}")]
    public async Task<IActionResult> DeleteRecipeSupply([FromRoute] Guid recipeId, [FromRoute] Guid supplyId)
    {
        await recipeCommandService.Handle(new DeleteRecipeSupplyCommand(recipeId, supplyId));
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