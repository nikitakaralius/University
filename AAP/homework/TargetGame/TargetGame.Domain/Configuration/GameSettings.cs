namespace TargetGame.Domain.Configuration;

/// <summary>
/// Настройки игры.
/// </summary>
public sealed class GameSettings
{
    public int TargetRadius { get; private init; }

    public int NumberOfSections { get; private init; }

    public int SectionWidth { get; private init; }

    public int AimingDelay { get; private init; }

    private GameSettings() { }

    public static readonly GameSettings Default = new()
    {
        TargetRadius = 15,
        NumberOfSections = 10,
        SectionWidth = 1,
        AimingDelay = 30
    };

    /// <summary>
    /// Создает валидный объект игровых настроек.
    /// </summary>
    /// <param name="targetRadius">Радиус мишени.</param>
    /// <param name="numberOfSections">Количество секций мишени.</param>
    /// <param name="sectionWidth">Ширина секции мишени.</param>
    /// <param name="aimingDelay">Задержка при прицеливании.</param>
    /// <returns>Игровые настройки.</returns>
    public static GameSettings Create(int targetRadius, int numberOfSections, int sectionWidth, int aimingDelay)
    {
        ThrowIfOutOfRange(targetRadius, 1, 50);
        ThrowIfOutOfRange(numberOfSections, 1, 50);
        ThrowIfOutOfRange(sectionWidth, 1, 10);
        ThrowIfOutOfRange(aimingDelay, 10, 300);
        ThrowIfOutOfRange(numberOfSections * sectionWidth, 1, targetRadius);

        return new GameSettings
        {
            TargetRadius = targetRadius,
            NumberOfSections = numberOfSections,
            SectionWidth = sectionWidth,
            AimingDelay = aimingDelay
        };
    }

    /// <summary>
    /// Выбрасывает исключение, если значения не попадает в заданный отрезок.
    /// </summary>
    /// <param name="parameter">Проверяемое значение</param>
    /// <param name="min">Нижняя граница</param>
    /// <param name="max">Верхняя граница</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private static void ThrowIfOutOfRange(int parameter, int min, int max)
    {
        if (parameter < min || parameter > max)
        {
            throw new ArgumentOutOfRangeException(nameof(parameter));
        }
    }
}
