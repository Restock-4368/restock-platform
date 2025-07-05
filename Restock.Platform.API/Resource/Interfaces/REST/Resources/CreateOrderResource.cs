namespace Restock.Platform.API.Resource.Interfaces.REST.Resources;

//Request resource
public record CreateOrderResource( 
    string? Description,
    int AdminRestaurantId,
    int SupplierId);