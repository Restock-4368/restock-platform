namespace Restock.Platform.API.Resource.Domain.Model.Entities;

public record OrderIdentifier(int Value)
{
    public OrderIdentifier() : this(0) {}

}