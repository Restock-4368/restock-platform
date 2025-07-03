using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Domain.Model.Entities;
using Restock.Platform.API.Resource.Domain.Repositories;
using Restock.Platform.API.Resource.Domain.Services;
using Restock.Platform.API.Shared.Domain.Repositories;

namespace Restock.Platform.API.Resource.Application.Internal.CommandServices;

public class BatchCommandService(IBatchRepository batchRepository, IUnitOfWork unitOfWork) : IBatchCommandService
{
    public async Task<Batch?> Handle(CreateBatchCommand command)
    {
        var batch = new Batch(command);
        await batchRepository.AddAsync(batch);
        await unitOfWork.CompleteAsync();
        return batch; 
    }

    public async Task Handle(UpdateBatchCommand command)
    {
        var batch = await batchRepository.FindByIdAsync(command.BatchId);

        if (batch is null)
            throw new Exception("Batch not found");

        batch.Update(
            command.Stock,
            command.ExpirationDate
        );

        batchRepository.Update(batch);
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(DeleteBatchCommand command)
    {
        var batch = await batchRepository.FindByIdAsync(command.BatchId);

        if (batch is null)
            throw new Exception("Batch not found");

        batchRepository.Remove(batch);
        await unitOfWork.CompleteAsync();
    }
}