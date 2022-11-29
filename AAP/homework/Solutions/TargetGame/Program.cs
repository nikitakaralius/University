using System;
using System.Threading.Tasks;
using TargetGame.ConsoleGame;
using TargetGame.Domain;

internal class Program
{
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

    private static void SubscribeToEvents(Game game)
    {
        game.OnUpdate += Console.Clear;
        game.OnTerminate += () => GameMessages.SayGoodBye(game.Score);
        game.Score.Changed += delta => GameMessages.DisplayScore(game.Score, delta);
    }
}
