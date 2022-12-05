using System;

internal class Program
{
    public static void Main()
    {
        try
        {
            Console.WriteLine("Введите последовательно координаты треугольника через ;");
            Console.WriteLine("Например, A = 2;3");

            Console.Write("A = ");
            var a = Vector2.Parse(Console.ReadLine()!);

            Console.Write("B = ");
            var b = Vector2.Parse(Console.ReadLine()!);

            Console.Write("C = ");
            var c = Vector2.Parse(Console.ReadLine()!);

            Console.WriteLine("Введите координаты точки, которую хотите проверить");
            Console.Write("P = ");
            var point = Vector2.Parse(Console.ReadLine()!);

            var triangle = Triangle.Create(a, b, c);
            Console.WriteLine(triangle.ContainsInside(point)
                                  ? "Точка находится внутри треугольнка"
                                  : "Точка находится вне треугольник");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

internal readonly struct Triangle
{
    public readonly Vector2 A, B, C;

    private Triangle(Vector2 a, Vector2 b, Vector2 c) => (A, B, C) = (a, b, c);

    public static Triangle Create(Vector2 a, Vector2 b, Vector2 c)
    {
        if (FormTriangle(a, b, c) == false)
        {
            throw new InvalidOperationException("Заданные точки не образуют треугольник");
        }

        return new Triangle(a, b, c);
    }

    /// <summary>
    /// Determines if is a point inside a triangle
    /// using barycentric coordinates.
    /// For more information see:
    /// https://en.wikipedia.org/wiki/Barycentric_coordinate_system
    /// </summary>
    /// <param name="point"></param>
    public bool ContainsInside(Vector2 point)
    {
        double s1 = C.Y - A.Y;
        double s2 = C.X - A.X;
        double s3 = B.Y - A.Y;
        double s4 = point.Y - A.Y;

        double w1 = (A.X * s1 + s4 * s2 - point.X * s1) / (s3 * s2 - (B.X - A.X) * s1);
        double w2 = (s4 - w1 * s3) / s1;
        return w1 >= 0 && w2 >= 0 && w1 + w2 <= 1;
    }

    /// <summary>
    /// Checks if is triangle area greater than zero
    /// </summary>
    private static bool FormTriangle(Vector2 a, Vector2 b, Vector2 c)
    {
        var ab = b - a;
        var ac = c - a;
        return Math.Abs(ab.X * ac.Y - ac.X * ab.Y) > 0;
    }
}

internal readonly struct Vector2
{
    public readonly double X, Y;

    public Vector2(double x, double y)
    {
        X = x;
        Y = y;
    }

    public static Vector2 Parse(string s)
    {
        string[] components = s.Split(';');

        if (components.Length < 2)
        {
            throw new ArgumentException("Координата должна состоять из двух компонент", nameof(s));
        }

        try
        {
            double x = double.Parse(components[0]);
            double y = double.Parse(components[1]);
            return new Vector2(x, y);
        }
        catch (Exception)
        {
            throw new ArgumentException("Одна из компонент не число");
        }
    }

    public static implicit operator Vector2((double x, double y) p) => new(p.x, p.y);

    public static Vector2 operator +(Vector2 a, Vector2 b) =>
        new(b.X + a.X, b.Y + a.Y);

    public static Vector2 operator -(Vector2 a, Vector2 b) =>
        new(b.X - a.X, b.Y - a.Y);
}
