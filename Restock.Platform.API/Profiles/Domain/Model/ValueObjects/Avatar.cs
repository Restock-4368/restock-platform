namespace Restock.Platform.API.Profiles.Domain.Model.ValueObjects;
 
public record Avatar
{
    public string Value { get; init; }

    public Avatar()
    {
        Value = "https://t4.ftcdn.net/jpg/02/15/84/43/360_F_215844325_ttX9YiIIyeaR7Ne6EaLLjMAmy4GvPC69.jpg";
    }
    public Avatar(string value)
    { 
        value = string.IsNullOrWhiteSpace(value)
            ? "https://t4.ftcdn.net/jpg/02/15/84/43/360_F_215844325_ttX9YiIIyeaR7Ne6EaLLjMAmy4GvPC69.jpg"
            : value;
 
        if (!Uri.IsWellFormedUriString(value, UriKind.Absolute))
            throw new ArgumentException("Invalid URL format.", nameof(value));

        Value = value;
    }
}