namespace Restock.Platform.API.Planning.Domain.Model.ValueObjects;

public record RecipeName(string Value)
{
    public override string ToString() => Value;
}