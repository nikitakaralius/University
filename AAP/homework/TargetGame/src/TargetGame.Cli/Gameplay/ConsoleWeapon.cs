namespace TargetGame.Cli.Gameplay;

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
