using System;
using System.Threading;
using System.Threading.Tasks;
using static System.ConsoleColor;

internal class Program
{
    // Этот файл - огромная свалка всех сущностей в проекте.
    // Рекомендуется просматривать не его, а решение в архиве,
    // где оно разбито на проекты и разные файлы.
    // Цель такой декомпозиции - разделить слой представления от слоя логики.
    // Благодаря такому разбиению, можно написать как консольное, так и графическое приложениия,
    // не меняя предыдущий код, что позоволяет легко масштабировать приложение.

    public static async Task Main(string[] args)
    {
        var game = GameBuilder.Create()
                              .UseSettings(_ => ConsoleConfiguration.AskPlayerForSettings())
                              .UseInput(_ => new ConsoleUserInput())
                              .UseWeapon(b => new ConsoleWeapon(b.Settings!.AimingDelay, b.Input!))
                              .Build();

        SubscribeToEvents(game);

        game.Start();
        await game.UpdateAsync();
        game.Terminate();
    }

    /// <summary>
    /// Делает подписки на события игры
    /// </summary>
    /// <param name="game"></param>
    private static void SubscribeToEvents(Game game)
    {
        game.OnUpdate += Console.Clear;
        game.OnTerminate += () => GameMessages.SayGoodBye(game.Score);
        game.Score.Changed += delta => GameMessages.DisplayScore(game.Score, delta);
    }
}

internal static class ConsoleUtils
{
    private const ConsoleColor DefaultColor = (ConsoleColor) (-1);
    public const int ErrorMessageDelay = 1500;

    /// <summary>
    /// Выводит сообщение на консоль с переносом строки.
    /// </summary>
    /// <param name="message">Сообщение.</param>
    /// <param name="color">Цвет сообщения.</param>
    /// <param name="time">Сколько времени показывается сообщение.</param>
    public static void PrintLine(string message, ConsoleColor color = DefaultColor, int time = 0)
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
    public static void Print(string message, ConsoleColor color = DefaultColor, int time = 0)
    {
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ForegroundColor = DefaultColor;
        if (time == 0) return;
        Thread.Sleep(time);
    }
}

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

internal sealed class ConsoleUserInput : IUserInput
{
    public const ConsoleKey TerminateKey = ConsoleKey.Spacebar;

    public bool StopAiming() => Console.KeyAvailable;

    public bool AskForTermination()
    {
        ConsoleUtils.PrintLine("Нажмите пробел, чтобы продолжить.", Yellow);
        ConsoleUtils.PrintLine("Нажмите любую другую клавишу, чтобы закончить.", Yellow);

        return Console.ReadKey(true).Key != TerminateKey;
    }
}

internal sealed class ConsoleWeapon : IWeapon
{
    private readonly int _aimingDelay;
    private readonly IUserInput _userInput;
    private readonly Random _random = new();

    public ConsoleWeapon(int aimingDelay, IUserInput userInput)
    {
        _aimingDelay = aimingDelay;
        _userInput = userInput;
    }

    public async Task<Point> ShootAsync(Target target)
    {
        ConsoleUtils.PrintLine("Прицеливаемся...", ConsoleColor.Yellow);
        var shootingPosition = await AimAsync(target);
        ConsoleUtils.PrintLine("Выстрел!...", ConsoleColor.Yellow);
        return shootingPosition;
    }

    /// <summary>
    /// Прицеливается по координатам (X; Y).
    /// </summary>
    /// <param name="target">Мишень, по которой прицеливается игрок.</param>
    /// <returns>Точка на мишени, куда прицелился игрок.</returns>
    private async Task<Point> AimAsync(Target target)
    {
        return new Point
        {
            X = await AimScalar(target, "X"),
            Y = await AimScalar(target, "Y")
        };
    }

    /// <summary>
    /// Прицеливается по одной компоненте.
    /// </summary>
    /// <param name="target">Мишень, по которой прицеливается игрок.</param>
    /// <param name="alias">Название компоненты.</param>
    /// <returns>Компонента на мишени, куда прицелился игрок.</returns>
    private async Task<double> AimScalar(Target target, string alias)
    {
        double scalar = _random.NextDouble(-target.Radius, target.Radius);
        int cursorPosition = Console.CursorTop;

        while (!_userInput.StopAiming())
        {
            scalar += _random.NextDouble() * _random.NextDouble();

            if (Math.Abs(scalar) > target.Radius)
            {
                scalar = -Math.Sign(scalar) * target.Radius;
            }

            ConsoleUtils.Print($"{alias} = {scalar}");
            Console.SetCursorPosition(0, cursorPosition);

            await Task.Delay(_aimingDelay);
        }

        Console.SetCursorPosition(0, cursorPosition + 1);
        Console.ReadKey(true);

        return scalar;
    }
}

internal sealed class GameMessages
{
    /// <summary>
    /// Сообщает количество очков игрока.
    /// </summary>
    /// <param name="score">Общеее количество очков.</param>
    /// <param name="delta">Изменение очков.</param>
    public static void DisplayScore(IScore score, int delta)
    {
        if (delta == 0)
        {
            ConsoleUtils.PrintLine("Промах!", ConsoleColor.Red);
        }
        else
        {
            ConsoleUtils.PrintLine($"Попадание! +{delta} к общему счету.", ConsoleColor.Green);
        }

        ConsoleUtils.PrintLine($"Общий счет: {score.Total}", ConsoleColor.White);
        Console.WriteLine();
    }

    /// <summary>
    /// Прощается с игроков.
    /// </summary>
    /// <param name="score">Общее количесто очков.</param>
    public static void SayGoodBye(IScore score)
    {
        Console.Clear();
        ConsoleUtils.PrintLine($"Ваш счет: {score.Total}. Заходите еще!", ConsoleColor.White);
        Console.ReadKey();
    }
}

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

public sealed class Game
{
    private readonly Target _target;
    private readonly Score _score;
    private readonly Player _player;
    private readonly IUserInput _input;

    internal Game(Target target, Score score, Player player, IUserInput input)
    {
        _target = target;
        _score = score;
        _player = player;
        _input = input;
    }

    public IScore Score => _score;

    public event Action? OnStart;
    public event Action? OnUpdate;
    public event Action? OnTerminate;

    /// <summary>
    /// Вызвается первым при запуске игры.
    /// Ответственен за настройку параметров игры.
    /// </summary>
    public void Start()
    {
        _player.Shot += OnPlayerShot;

        OnStart?.Invoke();
    }


    /// <summary>
    /// Основной цикл игры.
    /// </summary>
    public async Task UpdateAsync()
    {
        while (true)
        {
            await _player.ShootAsync(_target);

            bool wantsToTerminate = _input.AskForTermination();
            if (wantsToTerminate) break;

            OnUpdate?.Invoke();
        }
    }

    /// <summary>
    /// Вызывается перед выходом из игры.
    /// </summary>
    public void Terminate()
    {
        _player.Shot -= OnPlayerShot;

        OnTerminate?.Invoke();
    }

    /// <summary>
    /// Подписка на изменение очков игрока при выстреле в мишень
    /// </summary>
    /// <param name="hit"></param>
    private void OnPlayerShot(Point hit) => _score.Append(_target, hit);
}

public interface IWeapon
{
    /// <summary>
    /// Производит выстрел в выбранную позицию.
    /// </summary>
    /// <param name="target">Мишень, по которой стреляет игрок.</param>
    /// <returns>Точка на мишени, куда попал игрок.</returns>
    Task<Point> ShootAsync(Target target);
}

public sealed class Player
{
    private readonly IWeapon _weapon;

    public Player(IWeapon weapon)
    {
        _weapon = weapon;
    }

    public event Action<Point>? Shot;

    /// <summary>
    /// Производит выстрел в выбранную позицию.
    /// </summary>
    /// <param name="target">Мишень, по которой стреляет игрок.</param>
    /// <returns>Точка на мишени, куда попал игрок.</returns>
    public async Task ShootAsync(Target target)
    {
        var position = await _weapon.ShootAsync(target);
        Shot?.Invoke(position);
    }
}

public interface IScore
{
    event Action<int>? Changed;

    int Total { get; }
}

public sealed class Score : IScore
{
    public event Action<int>? Changed;

    public int Total { get; private set; }

    /// <summary>
    /// Считает очки при попадании и добавляет к общему счету.
    /// </summary>
    /// <param name="target">Мишень.</param>
    /// <param name="hit">Точка на мишени, куда попал игрок.</param>
    public void Append(Target target, Point hit)
    {
        int score = CalculateScore(target, hit);
        Total += score;
        Changed?.Invoke(score);
    }

    /// <summary>
    /// Считает количество очков после выстрела.
    /// </summary>
    /// <param name="target">Мишень, по которой стрелял игрок.</param>
    /// <param name="hit">Точка, куда попал игрок.</param>
    /// <returns>Количество очков за выстрел.</returns>
    private static int CalculateScore(Target target, Point hit)
    {
        double radius = Math.Sqrt(hit.X * hit.X + hit.Y * hit.Y);
        int hitSection = (int) Math.Floor(radius) / target.SectionWidth;
        int score = Math.Max(0, target.NumberOfSections - hitSection);
        return score;
    }
}

/// <summary>
/// Мишень.
/// </summary>
public sealed class Target
{
    public int Radius { get; init; }

    public int NumberOfSections { get; init; }

    public int SectionWidth { get; init; }
}

/// <summary>
/// Точка в декартовой системе координат.
/// </summary>
public readonly struct Point
{
    public double X { get; init; }

    public double Y { get; init; }
}

public interface IUserInput
{
    /// <summary>
    /// </summary>
    /// <returns>Перестал ли прицеливаться игрок</returns>
    bool StopAiming();

    /// <summary>
    /// Спрашивает игрока хочет ли он продолжить.
    /// </summary>
    /// <returns>
    /// true - если игрок нажал на пробел.
    /// false - во всех остальных случаях.
    /// </returns>
    bool AskForTermination();
}

public sealed class GameBuilder
{
    public GameSettings? Settings { get; private set; }

    public IWeapon? Weapon { get; private set; }

    public IUserInput? Input { get; private set; }

    /// <summary>
    /// Создает новый экземпляр GameBuilder
    /// </summary>
    /// <returns></returns>
    public static GameBuilder Create() => new();

    /// <summary>
    /// Собирает игру, используя заранее заданные параметры.
    /// </summary>
    /// <returns>Собранная игра.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Game Build()
    {
        if (Validate() == false)
        {
            throw new InvalidOperationException("Нелья собрать игру, когда не заданы все параметры.");
        }

        var target = new Target
        {
            Radius = Settings!.TargetRadius,
            NumberOfSections = Settings!.NumberOfSections,
            SectionWidth = Settings!.SectionWidth
        };

        var score = new Score();

        var player = new Player(Weapon!);

        return new Game(target, score, player, Input!);
    }

    /// <summary>
    /// Конфигурирует настройки игры.
    /// </summary>
    /// <param name="configure"></param>
    /// <returns>Тот же экземпляр GameBuilder</returns>
    public GameBuilder UseSettings(Func<GameBuilder, GameSettings> configure)
    {
        Settings = configure(this);
        return this;
    }

    /// <summary>
    /// Добавляет пользовательский ввод.
    /// </summary>
    /// <param name="configure"></param>
    /// <returns>Тот же экземпляр GameBuilder</returns>
    public GameBuilder UseInput(Func<GameBuilder, IUserInput> configure)
    {
        Input = configure(this);
        return this;
    }

    /// <summary>
    /// Добавляет оружие в игру.
    /// </summary>
    /// <param name="configure"></param>
    /// <returns>Тот же экземпляр GameBuilder</returns>
    public GameBuilder UseWeapon(Func<GameBuilder, IWeapon> configure)
    {
        Weapon = configure(this);
        return this;
    }

    /// <summary>
    /// Проверяет валидность состояния GameBuider.
    /// </summary>
    /// <returns>Валидность состояния.</returns>
    private bool Validate() =>
        Settings is not null &&
        Input is not null &&
        Weapon is not null;
}

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
