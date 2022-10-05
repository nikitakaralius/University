// C#10; NET 6.0; ImplicitUsings enabled
// ReSharper disable CheckNamespace

using static System.Console;

int number = ReadLine()!.AsInt();
RunExamples(number);

// RunExamples(11, 36, 46);

void RunExamples(params int[] seatNumbers)
{
    foreach (int seatNumber in seatNumbers)
    {
        RailcarSeat seat = new(seatNumber);
        WriteLine(seat.ToNbsFormat());
    }
}

internal sealed class RailcarSeat
{
    private const int CompartmentSeatsInRailcar = 36;

    public readonly int Number;

    public RailcarSeat(int number)
    {
        if (number is < 1 or > 54)
            throw new ArgumentOutOfRangeException(
                nameof(number), "Number should be in range [1; 54]");

        Number = number;
    }

    public bool IsTop => Number % 2 == 0;

    public bool IsBottom => IsTop == false;

    public bool IsCompartment => Number <= CompartmentSeatsInRailcar;

    public bool IsSide => IsCompartment == false;

    public int BlockNumber
    {
        get
        {
            const int compartmentSeatsInBlock = 4;
            const int sideSeatsInBlock = 2;
            const int blocksCount = 9;

            return IsCompartment
                ? (Number - 1) / compartmentSeatsInBlock + 1
                : (blocksCount + 1) - (Number - CompartmentSeatsInRailcar + 1) / sideSeatsInBlock;
        }
    }

    public string ToNbsFormat()
    {
        char type = IsCompartment ? 'к' : 'б';
        char position = IsTop ? 'в' : 'н';
        return $"{BlockNumber}{type}{position}";
    }
}

internal static class Extensions
{
    public static int AsInt(this string s) => int.Parse(s);
}
