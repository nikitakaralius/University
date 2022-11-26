using System;
using System.Linq;

internal class Program
{
    public static void Main(string[] args)
    {
        var (segmentWidth, parts) = ReadInput();
        TryDrawSegment(segmentWidth, parts);
    }

    private static (int SegmentWidth, int[] Parts) ReadInput()
    {
        int[] input = Console.ReadLine()!
                             .Split(", ")
                             .Select(x => int.Parse(x))
                             .ToArray();

        return (input[0], input[1..]);
    }

    private static void TryDrawSegment(int segmentWidth, int[] parts)
    {
        try
        {
            Console.WriteLine(BuildSegment(segmentWidth, parts));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private static string BuildSegment(int segmentWidth, int[] parts)
    {
        Span<char> segment = new(new string('-', segmentWidth).ToCharArray());
        int[] separatorIndexes = SeparatorIndexes(segmentWidth, parts);

        foreach (int separatorIndex in separatorIndexes)
        {
            segment[separatorIndex] = '|';
        }

        return segment.ToString();
    }

    private static int[] SeparatorIndexes(int segmentWidth, int[] parts)
    {
        int numberOfSeparators = parts.Length - 1;
        double divisionRank = (double) (segmentWidth - numberOfSeparators) / parts.Sum();

        int[] separatorIndexes = new int[numberOfSeparators];
        int offset = 0;

        for (int i = 0; i < numberOfSeparators; i++)
        {
            double segmentLength = Math.Round(divisionRank * parts[i]);

            if (segmentLength < 1)
            {
                throw new InvalidOperationException("Error");
            }

            separatorIndexes[i] = (int) segmentLength + offset;
            offset = separatorIndexes[i] + 1;
        }

        return separatorIndexes;
    }
}
