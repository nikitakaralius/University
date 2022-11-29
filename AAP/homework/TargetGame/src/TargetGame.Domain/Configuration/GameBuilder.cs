using TargetGame.Domain.Controls;
using TargetGame.Domain.Data;
using TargetGame.Domain.Gameplay;

namespace TargetGame.Domain.Configuration;

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
