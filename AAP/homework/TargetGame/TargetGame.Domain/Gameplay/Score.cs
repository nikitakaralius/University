using TargetGame.Domain.ValueObjects;

namespace TargetGame.Domain.Gameplay;

public interface IScore
{
    event Action<int>? Changed;

    int Total { get; }
}

public sealed class Score : IScore
{
    public event Action<int>? Changed;

    public int Total { get; private set; }

    public void Append(Target target, Point hit)
    {
        int score = CalculateScore(target, hit);
        Total += score;
        Changed?.Invoke(score);
    }

    /// <summary>
    /// Считает количество очков после выстрела.
    /// </summary>
    /// <param name="target">Мишень, по которой стрелял игрок.</param>
    /// <param name="hit">Точка, куда попал игрок.</param>
    /// <returns>Количество очков за выстрел.</returns>
    private static int CalculateScore(Target target, Point hit)
    {
        double radius = Math.Sqrt(hit.X * hit.X + hit.Y * hit.Y);
        int hitSection = (int) Math.Floor(radius) / target.SectionWidth;
        int score = Math.Max(0, target.NumberOfSections - hitSection);
        return score;
    }
}
