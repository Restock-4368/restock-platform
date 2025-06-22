namespace Restock.Platform.API.Resource.Domain.Model.ValueObjects;

using System;

public record StockRange
{
    private int MinStock { get; init; }
    private int MaxStock { get; init; }

    public StockRange(int minStock, int maxStock)
    {
        if (minStock < 0)
            throw new ArgumentException("minStock cannot be negative", nameof(minStock));
        if (maxStock < minStock)
            throw new ArgumentException("maxStock must be >= minStock", nameof(maxStock));

        MinStock = minStock;
        MaxStock = maxStock;
    }

    /// <summary>
    /// Devuelve true si el valor está dentro del rango [MinStock..MaxStock].
    /// </summary>
    public bool IsInRange(int value)
        => value >= MinStock && value <= MaxStock;
}