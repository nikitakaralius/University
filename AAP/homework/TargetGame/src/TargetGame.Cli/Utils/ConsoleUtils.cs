namespace TargetGame.Cli.Utils;

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
