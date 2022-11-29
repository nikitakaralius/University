using System;
using TargetGame.Domain;
using static System.ConsoleColor;

namespace TargetGame.ConsoleGame;

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
