using System;
using System.Collections.Generic;
using System.Linq;

public readonly struct Vector2
{
    public readonly double X, Y;

    public Vector2(double x, double y)
    {
        X = x;
        Y = y;
    }
}

public readonly struct Figure
{
    public readonly Vector2[] Vertices;
    public readonly int LineWidth;
    public readonly ConsoleColor Color;
    public readonly bool Filled;

    public Figure(Vector2[] vertices, int lineWidth, ConsoleColor color, bool filled)
    {
        Vertices = vertices;
        LineWidth = lineWidth;
        Color = color;
        Filled = filled;
    }
}

internal class Program
{
    private const double Tolerance = 0.0001;

    public static void Main(string[] args)
    {
        var filledIsoscelesTriangle = new Figure(new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(2, 5),
            new Vector2(4, 0)
        }, 1, ConsoleColor.Red, true);

        var filledEquilateralTriangle = new Figure(new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(0.5, Math.Sqrt(3) / 2),
            new Vector2(1, 0)
        }, 1, ConsoleColor.Blue, true);

        var notFilledIsoscelesTriangle = new Figure(new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(2, 5),
            new Vector2(4, 0)
        }, 1, ConsoleColor.Red, false);

        var rectangle = new Figure(new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(0, 2),
            new Vector2(4, 2),
            new Vector2(4, 0)
        }, 1, ConsoleColor.Red, true);

        var triangle = new Figure(new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(3, 5),
            new Vector2(4, 0)
        }, 1, ConsoleColor.Red, true);

        var figures = new Figure[]
        {
            filledIsoscelesTriangle,
            filledEquilateralTriangle,
            notFilledIsoscelesTriangle,
            rectangle,
            triangle
        };

        Console.WriteLine($"Количество равносторонних треугольников: {CountFilledIsoscelesTriangles(figures)}");
        Console.ReadKey();
    }

    private static int CountFilledIsoscelesTriangles(Figure[] figures)
    {
        int counter = 0;

        foreach (var figure in figures)
        {
            if (figure.Filled == false)
                continue;

            if (figure.Vertices.Length != 3)
                continue;

            double[] distances = CalculateDistances(figure.Vertices).ToArray();

            // Floating point comparison: https://www.jetbrains.com/help/resharper/CompareOfFloatsByEqualityOperator.html
            if (Math.Abs(distances[0] - distances[1]) < Tolerance || Math.Abs(distances[0] - distances[2]) < Tolerance)
                counter++;
        }

        return counter;
    }

    private static IEnumerable<double> CalculateDistances(Vector2[] vertices)
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            for (int j = i + 1; j < vertices.Length; j++)
            {
                double x = vertices[i].X - vertices[j].X;
                double y = vertices[i].Y - vertices[j].Y;

                yield return Math.Sqrt(x * x + y * y);
            }
        }
    }
}
