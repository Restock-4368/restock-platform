using Restock.Platform.API.Resource.Domain.Model.Commands;
using Restock.Platform.API.Resource.Domain.Model.Entities;

namespace Restock.Platform.API.Resource.Domain.Services;

public interface IBatchCommandService
{
    Task<Batch?> Handle(CreateBatchCommand command);
    Task Handle(UpdateBatchCommand command);
    Task Handle(DeleteBatchCommand command);
}