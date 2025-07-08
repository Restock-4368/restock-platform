namespace Restock.Platform.API.Resource.Domain.Model.ValueObjects;

public record SupplyIdentifier(int Value)
{
    public SupplyIdentifier() : this(0)
    {
    }
}