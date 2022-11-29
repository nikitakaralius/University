namespace TargetGame.Domain;

/// <summary>
/// Мишень.
/// </summary>
public sealed class Target
{
    public int Radius { get; init; }

    public int NumberOfSections { get; init; }

    public int SectionWidth { get; init; }
}
