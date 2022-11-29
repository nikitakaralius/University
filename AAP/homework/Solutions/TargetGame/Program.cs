using System;
using System.Threading;
using static System.ConsoleColor;

internal class Program
{
    private const ConsoleColor DefaultColor = (ConsoleColor) (-1);
    private const int ErrorMessageDelay = 1500;

    private static Target _target = null!;
    private static int _aimingDelay;
    private static int _totalScore;

    public static void Main(string[] args)
    {
        OnStart();
        OnUpdate();
        OnEnd();
    }

    /// <summary>
    /// Вызвается первым при запуске приложения.
    /// Ответственен за настройку параметров игры.
    /// </summary>
    private static void OnStart()
    {
        var settings = AskPlayerForSettings();

        _target = new Target
        {
            Radius = settings.TargetRadius,
            NumberOfSections = settings.NumberOfSections,
            SectionWidth = settings.SectionWidth
        };

        _aimingDelay = settings.AimingDelay;
    }

    /// <summary>
    /// Основной цикл игры.
    /// </summary>
    private static void OnUpdate()
    {
        while (true)
        {
            var shot = Shoot(_target, _aimingDelay);

            int score = CalculateScore(_target, shot);
            _totalScore += score;
            DisplayScore(_totalScore, score);

            bool wantToContinue = AskForContinuation();
            if (wantToContinue == false) break;

            Console.Clear();
        }
    }

    /// <summary>
    /// Вызывается перед выходом из игры.
    /// </summary>
    private static void OnEnd()
    {
        SayGoodBye(_totalScore);
    }

    /// <summary>
    /// Просит пользователя установить параметры игры.
    /// </summary>
    /// <returns>
    /// Пользовательские параметры.
    /// Параметры по умолчанию, если игрок отказался или ввел невалидные параметры.
    /// </returns>
    private static GameSettings AskPlayerForSettings()
    {
        Print("Используются стандартные настройки. Хотите их изменить? ");
        Print("[Y]es / ", Green);
        Print("[N]o ", Red);

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
            PrintLine("Пользовательские настройки", Green);

            Print("Введите радиус мишени (целое число [1; 50]): ");
            int targetRadius = int.Parse(Console.ReadLine()!);

            Print("Введите количество секций мишени (целое число [1; 10]): ");
            int numberOfSections = int.Parse(Console.ReadLine()!);

            Print("Введите ширину секции (целое число [1; 50]): ");
            int sectionWidth = int.Parse(Console.ReadLine()!);

            Print("Введите задержку (целое число [10; 300]): ");
            int delay = int.Parse(Console.ReadLine()!);

            var settings = GameSettings.Create(targetRadius, numberOfSections, sectionWidth, delay);

            Console.Clear();
            PrintLine("Настройки применены", Green, ErrorMessageDelay);

            return settings;
        }
        catch
        {
            Console.Clear();
            PrintLine("Введены некорректные данные. Будут использованы стандартные настройки.", Red, ErrorMessageDelay);
            return GameSettings.Default;
        }
    }

    /// <summary>
    /// Производит выстрел в выбранную позицию.
    /// </summary>
    /// <param name="target">Мишень, по которой стреляет игрок.</param>
    /// <param name="aimingDelay">Задержка при прицеливании.</param>
    /// <returns>Точка на мишени, куда попал игрок.</returns>
    private static Point Shoot(Target target, int aimingDelay)
    {
        PrintLine("Прицеливаемся...", Yellow);
        var shootingPosition = Aim(target, aimingDelay);
        PrintLine("Выстрел!...", Yellow);
        return shootingPosition;
    }

    /// <summary>
    /// Прицеливается по координатам (X; Y).
    /// </summary>
    /// <param name="target">Мишень, по которой прицеливается игрок.</param>
    /// <param name="aimingDelay">Задержка при прицеливании.</param>
    /// <returns>Точка на мишени, куда прицелился игрок.</returns>
    private static Point Aim(Target target, int aimingDelay)
    {
        double x = AimScalar(target, aimingDelay, "X");
        double y = AimScalar(target, aimingDelay, "Y");
        return new Point {X = x, Y = y};
    }

    /// <summary>
    /// Прицеливается по одной компоненте.
    /// </summary>
    /// <param name="target">Мишень, по которой прицеливается игрок.</param>
    /// <param name="aimingDelay">Задержка при прицеливании.</param>
    /// <param name="alias">Название компоненты.</param>
    /// <returns>Компонента на мишени, куда прицелился игрок.</returns>
    private static double AimScalar(Target target, int aimingDelay, string alias)
    {
        var random = new Random();
        double scalar = random.NextDouble(-target.Radius, target.Radius);
        int cursorPosition = Console.CursorTop;

        while (!Console.KeyAvailable)
        {
            scalar += random.NextDouble() * random.NextDouble();

            if (Math.Abs(scalar) > target.Radius)
            {
                scalar = -Math.Sign(scalar) * target.Radius;
            }

            PrintLine($"{alias} = {scalar}");
            Console.SetCursorPosition(0, cursorPosition);

            Thread.Sleep(aimingDelay);
        }

        Console.SetCursorPosition(0, cursorPosition + 1);
        Console.ReadKey(true);

        return scalar;
    }

    /// <summary>
    /// Считает количество очков после выстрела.
    /// </summary>
    /// <param name="target">Мишень, по которой стрелял игрок.</param>
    /// <param name="shot">Точка, куда попал игрок.</param>
    /// <returns>Количество очков за выстрел.</returns>
    private static int CalculateScore(Target target, Point shot)
    {
        double radius = Math.Sqrt(shot.X * shot.X + shot.Y * shot.Y);
        int hitSection = (int) Math.Floor(radius) / target.SectionWidth;
        int score = Math.Max(0, target.NumberOfSections - hitSection);
        return score;
    }

    /// <summary>
    /// Сообщает количество очков игрока.
    /// </summary>
    /// <param name="totalScore">Общеее количество очков.</param>
    /// <param name="delta">Изменение очков.</param>
    private static void DisplayScore(int totalScore, int delta)
    {
        if (delta == 0)
        {
            PrintLine("Промах!", Red);
        }
        else
        {
            PrintLine($"Попадание! +{delta} к общему счету.", Green);
        }

        PrintLine($"Общий счет: {totalScore}", White);
        Console.WriteLine();
    }

    /// <summary>
    /// Спрашивает игрока хочет ли он продолжить.
    /// </summary>
    /// <returns>
    /// true - если игрок нажал на пробел.
    /// false - во всех остальных случаях.
    /// </returns>
    private static bool AskForContinuation()
    {
        PrintLine("Нажмите пробел, чтобы продолжить.", Yellow);
        PrintLine("Нажмите любую другую клавишу, чтобы закончить.", Yellow);

        var input = Console.ReadKey(true).Key;

        return input == ConsoleKey.Spacebar;
    }

    /// <summary>
    /// Прощается с игроков.
    /// </summary>
    /// <param name="totalScore">Общее количесто очков.</param>
    private static void SayGoodBye(int totalScore)
    {
        Console.Clear();
        PrintLine($"Ваш счет: {totalScore}. Заходите еще!", White);
        Console.ReadKey();
    }

    /// <summary>
    /// Выводит сообщение на консоль с переносом строки.
    /// </summary>
    /// <param name="message">Сообщение.</param>
    /// <param name="color">Цвет сообщения.</param>
    /// <param name="time">Сколько времени показывается сообщение.</param>
    private static void PrintLine(string message, ConsoleColor color = DefaultColor, int time = 0)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = DefaultColor;
        if (time == 0) return;
        Thread.Sleep(time);
    }

    /// <summary>
    /// Выводит сообщение на консоль с переносом строки.
    /// </summary>
    /// <param name="message">Сообщение.</param>
    /// <param name="color">Цвет сообщения.</param>
    /// <param name="time">Сколько времени показывается сообщение.</param>
    private static void Print(string message, ConsoleColor color = DefaultColor, int time = 0)
    {
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ForegroundColor = DefaultColor;
        if (time == 0) return;
        Thread.Sleep(time);
    }
}

/// <summary>
/// Настройки игры.
/// </summary>
internal sealed class GameSettings
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

    public static GameSettings Create(int targetRadius, int numberOfSections, int sectionWidth, int delay)
    {
        ThrowIfOutOfRange(targetRadius, 1, 50);
        ThrowIfOutOfRange(numberOfSections, 1, 50);
        ThrowIfOutOfRange(sectionWidth, 1, 10);
        ThrowIfOutOfRange(delay, 10, 300);
        ThrowIfOutOfRange(numberOfSections * sectionWidth, 1, targetRadius);

        return new GameSettings
        {
            TargetRadius = targetRadius,
            NumberOfSections = numberOfSections,
            SectionWidth = sectionWidth,
            AimingDelay = delay
        };
    }

    private static void ThrowIfOutOfRange(int parameter, int min, int max)
    {
        if (parameter < min || parameter > max)
        {
            throw new ArgumentOutOfRangeException(nameof(parameter));
        }
    }
}

/// <summary>
/// Точка в декартовой системе координат.
/// </summary>
internal readonly struct Point
{
    public double X { get; init; }

    public double Y { get; init; }
}

/// <summary>
/// Мишень.
/// </summary>
internal sealed class Target
{
    public int Radius { get; init; }

    public int NumberOfSections { get; init; }

    public int SectionWidth { get; init; }
}

/// <summary>
/// Методы расширения для класса Random.
/// </summary>
internal static class RandomExtensions
{
    public static double NextDouble(this Random random, int minValue, int maxValue)
    {
        return random.NextDouble() * (maxValue - minValue) + minValue;
    }
}
