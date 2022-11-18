using System;

internal class Program
{
    public static void Main(string[] args)
    {
        int date = int.Parse(Console.ReadLine()!);
        int answer = Numerology(date);
        Console.WriteLine(answer);
        Console.ReadKey();
    }

    private static int Numerology(int date)
    {
        int digitsSum = DigitsSum(date);

        while (digitsSum / 10 > 0)
            digitsSum = DigitsSum(digitsSum);

        return digitsSum;
    }

    private static int DigitsSum(int value)
    {
        int sum = 0;

        while (value > 0)
        {
            sum += value % 10;
            value /= 10;
        }

        return sum;
    }
}
