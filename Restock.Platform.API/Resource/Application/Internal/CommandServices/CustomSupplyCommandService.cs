using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Domain.Repositories;
using Restock.Platform.API.Resource.Domain.Services;
using Restock.Platform.API.Shared.Domain.Repositories;

namespace Restock.Platform.API.Resource.Application.Internal.CommandServices;

public class CustomSupplyCommandService(ICustomSupplyRepository customSupplyRepository, IUnitOfWork unitOfWork) : ICustomSupplyCommandService
{
    public async Task<CustomSupply?> Handle(CreateCustomSupplyCommand command)
    {
        var customSupply = new CustomSupply(command);
        await customSupplyRepository.AddAsync(customSupply);
        await unitOfWork.CompleteAsync();
        return customSupply; 
    }

    public async Task Handle(UpdateCustomSupplyCommand command)
    {
        var customSupply = await customSupplyRepository.FindByIdAsync(command.CustomSupplyId);

        if (customSupply is null)
            throw new Exception("Custom Supply not found");

        customSupply.Update(
            command.Description,
            command.Perishable,
            command.MinStock,
            command.MaxStock,
            command.Price 
        );
         
        customSupplyRepository.Update(customSupply);
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(DeleteCustomSupplyCommand command)
    {
        var customSupply = await customSupplyRepository.FindByIdAsync(command.CustomSupplyId);

        if (customSupply is null)
            throw new Exception("Custom Supply not found");

        customSupplyRepository.Remove(customSupply);
        await unitOfWork.CompleteAsync();
    }
}