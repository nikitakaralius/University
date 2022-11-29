using System;

namespace TargetGame.Cli;

/// <summary>
/// Методы расширения для класса Random.
/// </summary>
internal static class RandomExtensions
{
    /// <summary>
    /// Создает случайное число в полуинтервале от minValue до maxValue
    /// </summary>
    /// <param name="random">Random</param>
    /// <param name="minValue">Нижняя граница</param>
    /// <param name="maxValue">Верхняя граница</param>
    /// <returns>Случайное число</returns>
    public static double NextDouble(this Random random, int minValue, int maxValue)
    {
        return random.NextDouble() * (maxValue - minValue) + minValue;
    }
}
