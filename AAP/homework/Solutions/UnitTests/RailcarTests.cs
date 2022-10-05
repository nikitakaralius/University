using FluentAssertions;

namespace TrainTests;

public class RailcarSeatTests
{
    [Test]
    public void CompartmentsShouldBeInCorrectBlock()
    {
        var compartment = Enumerable.Range(1, 36)
                                    .Select(x => new RailcarSeat(x))
                                    .Chunk(4)
                                    .ToArray();

        for (int i = 0; i < compartment.Length; i++)
            compartment[i]
                .Should()
                .OnlyContain(seat => seat.BlockNumber == i + 1);
    }

    [Test]
    public void SidesShouldBeInCorrectBlock()
    {
        var compartment = Enumerable.Range(37, 54 - 36)
                                    .Reverse()
                                    .Select(x => new RailcarSeat(x))
                                    .Chunk(2)
                                    .ToArray();

        for (int i = 0; i < compartment.Length; i++)
            compartment[i]
                .Should()
                .OnlyContain(seat => seat.BlockNumber == i + 1);
    }
}
