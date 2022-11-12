using System;

internal class Program
{
    public static void Main(string[] args)
    {
        int value, count = 0;
        double sum = 0;

        do
        {
            value = int.Parse(Console.ReadLine()!);
            sum += value;
            count++;
        }
        while (value != 0);

        Console.WriteLine(sum / (count - 1));
        Console.ReadKey();
    }
}
