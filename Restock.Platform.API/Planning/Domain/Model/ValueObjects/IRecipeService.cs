using Restock.Platform.API.Planning.Domain.Model.Commands;

namespace Restock.Platform.API.Planning.Domain.Model.ValueObjects;

public interface IRecipeService
{
    Task<Guid> Handle(CreateRecipeCommand command);
    Task Handle(AddSupplyToRecipeCommand command);
}
