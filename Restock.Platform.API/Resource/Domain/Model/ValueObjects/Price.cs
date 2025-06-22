namespace Restock.Platform.API.Resource.Domain.Model.ValueObjects;
using System;

public record Price
{
    private decimal Amount { get; init; }
    private string Currency { get; init; }

    public Price(decimal amount, string currency)
    {
        if (amount < 0)
            throw new ArgumentException("Price cannot be negative", nameof(amount));
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency cannot be null or empty", nameof(currency));

        Amount = amount;
        Currency = currency;
    }

    /// <summary>
    /// Devuelve una nueva instancia de Price con el descuento aplicado.
    /// </summary>
    public Price ApplyDiscount(decimal discountPercentage)
    {
        if (discountPercentage < 0 || discountPercentage > 100)
            throw new ArgumentException("Invalid discount percentage", nameof(discountPercentage));

        var discountedAmount = Amount * (1 - discountPercentage / 100);
        return new Price(discountedAmount, Currency);
    }
}