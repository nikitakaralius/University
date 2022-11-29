using System;
using TargetGame.Domain;

namespace TargetGame.Cli;

internal static class ConsoleConfiguration
{
    /// <summary>
    /// Просит пользователя установить параметры игры.
    /// </summary>
    /// <returns>
    /// Пользовательские параметры.
    /// Параметры по умолчанию, если игрок отказался или ввел невалидные параметры.
    /// </returns>
    public static GameSettings AskPlayerForSettings()
    {
        ConsoleUtils.Print("Используются стандартные настройки. Хотите их изменить? ");
        ConsoleUtils.Print("[Y]es / ", ConsoleColor.Green);
        ConsoleUtils.Print("[N]o ", ConsoleColor.Red);

        var input = Console.ReadKey(true).Key;
        Console.Clear();

        GameSettings settings = input switch
        {
            ConsoleKey.Y => ReadGameSettings(),
            _            => GameSettings.Default
        };

        Console.Clear();

        return settings;
    }

    /// <summary>
    /// Считывает параметры игрока с клавиатуры.
    /// </summary>
    /// <returns>Пользовательские параметры.</returns>
    private static GameSettings ReadGameSettings()
    {
        try
        {
            ConsoleUtils.PrintLine("Пользовательские настройки", ConsoleColor.Green);

            ConsoleUtils.Print("Введите радиус мишени (целое число [1; 50]): ");
            int targetRadius = int.Parse(Console.ReadLine()!);

            ConsoleUtils.Print("Введите количество секций мишени (целое число [1; 10]): ");
            int numberOfSections = int.Parse(Console.ReadLine()!);

            ConsoleUtils.Print("Введите ширину секции (целое число [1; 50]): ");
            int sectionWidth = int.Parse(Console.ReadLine()!);

            ConsoleUtils.Print("Введите задержку (целое число [10; 300]): ");
            int delay = int.Parse(Console.ReadLine()!);

            var settings = GameSettings.Create(targetRadius, numberOfSections, sectionWidth, delay);

            Console.Clear();
            ConsoleUtils.PrintLine("Настройки применены", ConsoleColor.Green, ConsoleUtils.ErrorMessageDelay);

            return settings;
        }
        catch
        {
            Console.Clear();
            ConsoleUtils.PrintLine("Введены некорректные данные. Будут использованы стандартные настройки.", ConsoleColor.Red,
                                   ConsoleUtils.ErrorMessageDelay);
            return GameSettings.Default;
        }
    }
}
