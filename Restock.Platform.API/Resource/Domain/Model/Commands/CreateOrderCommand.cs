namespace Restock.Platform.API.Resource.Domain.Model.Commands;

public record CreateOrderCommand(
    string? Description,
    int AdminRestaurantId,
    int SupplierId);