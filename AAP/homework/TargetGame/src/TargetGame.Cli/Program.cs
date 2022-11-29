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
