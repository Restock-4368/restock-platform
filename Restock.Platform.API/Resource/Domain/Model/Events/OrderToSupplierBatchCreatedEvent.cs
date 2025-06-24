using Restock.Platform.API.Shared.Domain.Events;

namespace Restock.Platform.API.Resource.Domain.Model.Events;

public class OrderToSupplierBatchCreatedEvent(int batchId, double quantity, bool accepted) : IEvent
{
    
}