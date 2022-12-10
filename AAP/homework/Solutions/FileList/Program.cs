using System;
using System.IO;

internal class Program
{
    private const string Tab = "  ";
    private const string DirectorySymbol = "+";
    private const string FileSymbol = "├";

    public static void Main(string[] args)
    {
        Console.Write("Введите путь к директории: ");
        var directory = new DirectoryInfo(Console.ReadLine()!);
        PrintAllFiles(directory);
        Console.ReadKey();
    }

    private static void PrintAllFiles(DirectoryInfo directory, string tabs = "")
    {
        PrintLine($"{tabs}{DirectorySymbol} {directory.Name}", ConsoleColor.Yellow);

        foreach (var file in directory.GetFiles())
        {
            Console.WriteLine($"{tabs}{Tab}{FileSymbol} {file.Name}");
        }

        foreach (var subDirectory in directory.GetDirectories())
        {
            PrintAllFiles(subDirectory, tabs + Tab);
        }
    }

    private static void PrintLine(string message, ConsoleColor color = (ConsoleColor) (-1))
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = (ConsoleColor) (-1);
    }
}
