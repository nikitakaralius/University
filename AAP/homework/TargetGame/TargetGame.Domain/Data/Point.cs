namespace TargetGame.Domain.ValueObjects;

/// <summary>
/// Точка в декартовой системе координат.
/// </summary>
public readonly struct Point
{
    public double X { get; init; }

    public double Y { get; init; }
}
