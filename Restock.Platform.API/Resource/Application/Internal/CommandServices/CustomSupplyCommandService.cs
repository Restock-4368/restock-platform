using Restock.Platform.API.Resource.Domain.Model.Aggregates;
using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Domain.Repositories;
using Restock.Platform.API.Resource.Domain.Services;
using Restock.Platform.API.Shared.Domain.Exceptions;
using Restock.Platform.API.Shared.Domain.Repositories;

namespace Restock.Platform.API.Resource.Application.Internal.CommandServices;

public class CustomSupplyCommandService(
    ICustomSupplyRepository customSupplyRepository, 
    IBatchRepository batchRepository,
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : ICustomSupplyCommandService
{
    public async Task<CustomSupply?> Handle(CreateCustomSupplyCommand command)
    {
        if (await customSupplyRepository.ExistsBySupplyIdAndUserIdAsync(command.SupplyId, command.UserId))
            throw new BusinessRuleException("CustomSupply already exists for this supply and user.");
        
        var customSupply = new CustomSupply(command);
        await customSupplyRepository.AddAsync(customSupply);
        await unitOfWork.CompleteAsync();
        return customSupply; 
    }

    public async Task Handle(UpdateCustomSupplyCommand command)
    {
        var customSupply = await customSupplyRepository.FindByIdAsync(command.CustomSupplyId);

        if (customSupply is null)
            throw new BusinessRuleException("Custom Supply not found");

        customSupply.Update(
            command.Description,
            command.MinStock,
            command.MaxStock,
            command.Price 
        );
         
        customSupplyRepository.Update(customSupply);
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(DeleteCustomSupplyCommand command)
    {
        var batches = await batchRepository.ListByCustomSupplyId(command.CustomSupplyId);

        foreach (var batch in batches)
        {
            if (await orderRepository.ExistsByBatchId(batch.Id))
                throw new BusinessRuleException("Cannot delete CustomSupply because it has a batch used in an order.");
        }
        
        var customSupply = await customSupplyRepository.FindByIdAsync(command.CustomSupplyId);

        if (customSupply is null)
            throw new BusinessRuleException("Custom Supply not found");

        customSupplyRepository.Remove(customSupply);
        await unitOfWork.CompleteAsync();
    }
}