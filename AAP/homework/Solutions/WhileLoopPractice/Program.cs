using System;

internal class Program
{
    public static void Main(string[] args)
    {
        const double timeStep = 20.0 / 60.0;

        double volume = double.Parse(Console.ReadLine()!);
        double sailorSpeed = double.Parse(Console.ReadLine()!);
        double waterSpeed = double.Parse(Console.ReadLine()!);

        double sailorWorkIncrement = int.Parse(Console.ReadLine()!) / 100.0;
        double waterWorkIncrement = int.Parse(Console.ReadLine()!) / 100.0;

        double requiredTime = 0;

        while (volume > 0)
        {
            volume -= sailorSpeed * timeStep;
            volume += waterSpeed * timeStep;

            sailorSpeed += sailorSpeed * sailorWorkIncrement;
            waterSpeed += waterSpeed * waterWorkIncrement;

            requiredTime += timeStep;
        }

        Console.WriteLine((int) requiredTime);
        Console.ReadKey();
    }
}
