using static System.Console;

WriteLine("Enter Box Sizes separated by a space (e.g. 1 2 3)");
double[] boxSizes = ReadLine()!.AsDoubleArray();
WriteLine("Enter Container Sizes separated by a space (e.g. 1 2 3)");
double[] containerSizes = ReadLine()!.AsDoubleArray();

Box box = new(
    height: boxSizes[0],
    width: boxSizes[1],
    depth: boxSizes[2]);

Box container = new(
    height: containerSizes[0],
    width: containerSizes[1],
    depth: containerSizes[2]);

WriteLine(box.FistIn(container)
              ? "Yes, you can put this box in the container"
              : "No, the box is too large to be in the container");

internal sealed class Box
{
    public readonly double Height, Width, Depth;

    public Box(double height, double width, double depth) =>
        (Height, Width, Depth) = (height, width, depth);

    public bool FistIn(Box container)
    {
        bool FitsInContainer(Box box) =>
            box.Height <= container.Height &&
            box.Width <= container.Width &&
            box.Depth <= container.Depth;

        return Permutations().Any(b => FitsInContainer(b));
    }

    private Box[] Permutations()
    {
        return new[]
        {
            this,
            new(Width, Depth, Height),
            new(Height, Width, Depth),
            new(Height, Depth, Width),
            new(Depth, Height, Width),
            new(Depth, Width, Height)
        };
    }
}

internal static class Extensions
{
    public static double AsDouble(this string s) => double.Parse(s);

    public static double[] AsDoubleArray(this string s) =>
        s.Split()
         .Select(x => x.AsDouble())
         .ToArray();
}
