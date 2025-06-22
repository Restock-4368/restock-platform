namespace Restock.Platform.API.Planning.Domain.Model.ValueObjects;

public record RecipeImageURL
{
    public string Value { get; init; }

    public RecipeImageURL()
    {
        Value = string.Empty;
    }
    public RecipeImageURL(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !Uri.IsWellFormedUriString(value, UriKind.Absolute))
            throw new ArgumentException("Invalid URL format.", nameof(value));
        Value = value;
    }
}