using System;

internal class Program
{
    public static void Main(string[] args)
    {
        int currentAge, previousAge = 0;
        int sequenceLength = 0, longestSequenceLength = 0;

        do
        {
            currentAge = int.Parse(Console.ReadLine()!);

            if (currentAge != previousAge)
                sequenceLength = 0;

            sequenceLength++;
            previousAge = currentAge;

            longestSequenceLength = Math.Max(sequenceLength, longestSequenceLength);
        }
        while (currentAge != 0);

        Console.WriteLine(longestSequenceLength);
    }
}
