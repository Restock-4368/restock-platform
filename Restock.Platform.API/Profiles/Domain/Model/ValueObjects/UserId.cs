using Restock.Platform.API.Shared.Domain.Exceptions;

namespace Restock.Platform.API.Profiles.Domain.Model.ValueObjects;

public record UserId
{
    public int Value { get; }

    public UserId(int value)
    {
        if (value <= 0)
            throw new BusinessRuleException("UserId must be a positive number greater than 0.");

        Value = value;
    }
}