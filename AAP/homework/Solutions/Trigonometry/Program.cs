using System;

internal class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Enter x (rad): ");
        double x = double.Parse(Console.ReadLine()!);
        Console.Write("Enter epsilon: ");
        double epsilon = double.Parse(Console.ReadLine()!);

        double cos = TaylorSeriesMath.Cos(x, epsilon);
        Console.WriteLine($"Cos of {x} is equals to {cos}");
    }
}

internal static class TaylorSeriesMath
{
    public static double Sin(double x, double epsilon = 1E-3) =>
        BaseTrigonometrySeries(x, initialPower: 1, initialSeriesMember: x, epsilon);

    public static double Cos(double x, double epsilon = 1E-3) =>
        BaseTrigonometrySeries(x, initialPower: 0, initialSeriesMember: 1, epsilon);

    private static double BaseTrigonometrySeries(double x, int initialPower, double initialSeriesMember, double epsilon)
    {
        const int step = 2;
        const int guaranteedIterations = 3;

        int power = initialPower, sign = 1;
        ulong factorial = 1;
        double result = 0, seriesMember = initialSeriesMember;

        bool GuaranteeIterations() => power <= step * guaranteedIterations - 1;

        while (GuaranteeIterations() || Math.Abs(seriesMember) >= epsilon)
        {
            result += seriesMember;

            int nextPower = power + step;
            for (int v = power + 1; v <= nextPower; v++)
                factorial *= (ulong) v;

            power = nextPower;
            sign *= -1;

            seriesMember = sign * Math.Pow(x, power) / factorial;
        }

        return result;
    }
}
