using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Domain.Model.Entities;
using Restock.Platform.API.Resource.Domain.Repositories;
using Restock.Platform.API.Resource.Domain.Services;
using Restock.Platform.API.Shared.Domain.Exceptions;
using Restock.Platform.API.Shared.Domain.Repositories;

namespace Restock.Platform.API.Resource.Application.Internal.CommandServices;

public class BatchCommandService(
    IBatchRepository batchRepository, 
    IOrderRepository orderRepository,
    ICustomSupplyRepository customSupplyRepository,
    IUnitOfWork unitOfWork) : IBatchCommandService
{
    public async Task<Batch?> Handle(CreateBatchCommand command)
    {
        var customSupply = await customSupplyRepository.FindByIdAsync(command.CustomSupplyId);
        if (customSupply is null)
            throw new BusinessRuleException("Custom supply not found");
    
        if (customSupply.UserId != command.UserId)
            throw new BusinessRuleException("Custom supply does not belong to this user");

        // Validar por SupplyId y UserId
        if (await batchRepository.ExistsBySupplyIdAndUserIdAsync(customSupply.SupplyId, command.UserId))
            throw new BusinessRuleException("Batch already exists for this supply and user.");
        
        var batch = new Batch(command);
        await batchRepository.AddAsync(batch);
        await unitOfWork.CompleteAsync();
        return batch; 
    }

    public async Task Handle(UpdateBatchCommand command)
    {
        var batch = await batchRepository.FindByIdAsync(command.BatchId);

        if (batch is null)
            throw new BusinessRuleException("Batch not found");

        batch.Update(
            command.Stock,
            command.ExpirationDate
        );

        batchRepository.Update(batch);
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(DeleteBatchCommand command)
    {
        if (await orderRepository.ExistsByBatchId(command.BatchId))
            throw new BusinessRuleException("Cannot delete batch because it is referenced by an order.");
        
        var batch = await batchRepository.FindByIdAsync(command.BatchId);

        if (batch is null)
            throw new BusinessRuleException("Batch not found");

        batchRepository.Remove(batch);
        await unitOfWork.CompleteAsync();
    }
}