using TargetGame.Domain.Gameplay;
using TargetGame.Domain.Input;
using TargetGame.Domain.ValueObjects;

namespace TargetGame.Domain.Configuration;

public sealed class GameBuilder
{
    public GameSettings? Settings { get; private set; }

    public IWeapon? Weapon { get; private set; }

    public IUserInput? Input { get; private set; }

    public static GameBuilder Create() => new();

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

    public GameBuilder UseSettings(Func<GameBuilder, GameSettings> configure)
    {
        Settings = configure(this);
        return this;
    }

    public GameBuilder UseInput(Func<GameBuilder, IUserInput> configure)
    {
        Input = configure(this);
        return this;
    }

    public GameBuilder UseWeapon(Func<GameBuilder, IWeapon> configure)
    {
        Weapon = configure(this);
        return this;
    }

    private bool Validate() =>
        Settings is not null &&
        Input is not null &&
        Weapon is not null;
}
