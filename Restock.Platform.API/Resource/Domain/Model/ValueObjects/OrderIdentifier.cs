namespace Restock.Platform.API.Resource.Domain.Model.Entities;

public record OrderIdentifier(Guid Value)
{
    public OrderIdentifier() : this(Guid.NewGuid()) {}

}