using Restock.Platform.API.Planning.Domain.Model.Commands;

namespace Restock.Platform.API.Planning.Domain.Services;

public interface IRecipeCommandService
{
    Task<Guid> Handle(CreateRecipeCommand command);
    Task Handle(UpdateRecipeCommand command);
    Task Handle(DeleteRecipeCommand command);
    Task Handle(AddRecipeSupplyCommand command);
    Task Handle(UpdateRecipeSupplyCommand command);
    Task Handle(DeleteRecipeSupplyCommand command);
}