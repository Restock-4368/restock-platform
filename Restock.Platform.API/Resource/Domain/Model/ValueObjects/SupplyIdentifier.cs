namespace Restock.Platform.API.Resource.Domain.Model.ValueObjects;

public record SupplyIdentifier(Guid Value)
{
    public SupplyIdentifier() : this(Guid.NewGuid())
    {
    }
}