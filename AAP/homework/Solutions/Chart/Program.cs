// NET 6.0; C# 10; ImplicitUsings enabled

double x = Console.ReadLine()!.AsDouble();
double result = SecondVariant(x);
Console.WriteLine(Math.Round(result, 3));

double FirstVariant(double x)
{
    const int period = 2;
    const int amplitude = 1;
    const int bias = 2;

    double xMappedOnPeriod = Math.Abs(x) % period;

    return xMappedOnPeriod <= amplitude
        ? xMappedOnPeriod
        : bias - xMappedOnPeriod;
}

double SecondVariant(double x) => FirstVariant(x) - 1;

internal static class Extensions
{
    public static double AsDouble(this string s) => double.Parse(s);
}

