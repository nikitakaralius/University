// .NET 6.0; C# 10; ImplicitUsings enabled

using static System.Console;

ReadLine()!
    .AsInt()
    .Checksum()
    .WriteToConsole();

internal static class Extensions
{
    public static int AsInt(this string s) => int.Parse(s);

    public static void WriteToConsole(this int x) => WriteLine(x);

    public static int Checksum(this int number)
    {
        int SumOfDigits(int value)
        {
            int sumOfDigits = 0;

            while (value != 0)
            {
                sumOfDigits += value % 10;
                value /= 10;
            }

            return sumOfDigits;
        }

        return SumOfDigits(number) % 10;
    }
}
