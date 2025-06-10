namespace Restock.Platform.API.Resource.Domain.Model.ValueObjects;

public class SupplyIdentifier(Guid Value)
{
    public SupplyIdentifier() : this(Guid.NewGuid())
    {
    }
}