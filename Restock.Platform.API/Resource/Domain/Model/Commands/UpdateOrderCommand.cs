using Restock.Platform.API.Resource.Domain.Model.Entities;

namespace Restock.Platform.API.Resource.Domain.Model.Commands;

public record UpdateOrderCommand(
    int OrderId,
    DateTime? EstimatedShipDate,
    DateTime? EstimatedShipTime,
    string? Description
); 