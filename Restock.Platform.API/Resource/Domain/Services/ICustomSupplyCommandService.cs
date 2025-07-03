using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Domain.Model.Commands;

namespace Restock.Platform.API.Resource.Domain.Services;

public interface ICustomSupplyCommandService
{
    Task<CustomSupply?> Handle(CreateCustomSupplyCommand command);
    Task Handle(UpdateCustomSupplyCommand command);
    Task Handle(DeleteCustomSupplyCommand command);
}