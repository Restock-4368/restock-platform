using Restock.Platform.API.Planning.Domain.Model.Aggregates;
using Restock.Platform.API.Planning.Domain.Model.Commands;
using Restock.Platform.API.Planning.Domain.Model.Entities;
using Restock.Platform.API.Planning.Domain.Model.ValueObjects;
using Restock.Platform.API.Planning.Domain.Repositories;
using Restock.Platform.API.Planning.Domain.Services;
using Restock.Platform.API.Resource.Domain.Model.ValueObjects;
using Restock.Platform.API.Shared.Domain.Repositories;

namespace Restock.Platform.API.Planning.Application.Internal.CommandServices;

public class RecipeCommandService(
    IRecipeRepository recipeRepository,
    IUnitOfWork unitOfWork) : IRecipeCommandService
{
    public async Task<Guid> Handle(CreateRecipeCommand command)
    {
        var recipe = new Recipe(
            new RecipeIdentifier(),
            command.Name,
            command.Description,
            new RecipeImageURL(command.ImageUrl),
            new RecipePrice(command.TotalPrice),
            command.UserId);
        
        await recipeRepository.AddAsync(recipe);
        await unitOfWork.CompleteAsync();

        return recipe.Id.Value;
    }

    public async Task Handle(UpdateRecipeCommand command)
    {
        var existing = await recipeRepository.FindByIdAsync(command.Id);
        if (existing == null) throw new KeyNotFoundException("Recipe not found");
        
        existing.Update(
            command.Name,
            command.Description,
            new RecipeImageURL(command.ImageUrl),
            new RecipePrice(command.TotalPrice));
        
        recipeRepository.Update(existing);  
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(DeleteRecipeCommand command)
    {
        var existing = await recipeRepository.FindByIdAsync(command.Id);
        if (existing == null) throw new KeyNotFoundException("Recipe not found");

        recipeRepository.Remove(existing);
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(AddRecipeSupplyCommand command)
    {
        var recipe = await recipeRepository.FindByIdAsync(command.RecipeId);
        if (recipe is null) 
            throw new KeyNotFoundException("Recipe not found");
        recipe.AddSupply(
            recipe.Id,
            new SupplyIdentifier(command.SupplyId), 
            new RecipeQuantity(command.Quantity));

        recipeRepository.Update(recipe); 
        await unitOfWork.CompleteAsync();
    }
}