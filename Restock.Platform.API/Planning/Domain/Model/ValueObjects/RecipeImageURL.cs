namespace Restock.Platform.API.Planning.Domain.Model.ValueObjects;

public record RecipeImageURL(string Value)
{
    public RecipeImageURL() : this(string.Empty)
    {
        if(string.IsNullOrWhiteSpace(Value) || !Uri.IsWellFormedUriString(Value, UriKind.Absolute))
        {
            throw new ArgumentException("Invalid URL format.", nameof(Value));
        }
    }
}