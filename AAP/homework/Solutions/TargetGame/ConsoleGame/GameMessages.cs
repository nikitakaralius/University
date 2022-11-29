using System;
using TargetGame.Domain;

namespace TargetGame.ConsoleGame;

internal sealed class GameMessages
{
    /// <summary>
    /// Сообщает количество очков игрока.
    /// </summary>
    /// <param name="score">Общеее количество очков.</param>
    /// <param name="delta">Изменение очков.</param>
    public static void DisplayScore(IScore score, int delta)
    {
        if (delta == 0)
        {
            ConsoleUtils.PrintLine("Промах!", ConsoleColor.Red);
        }
        else
        {
            ConsoleUtils.PrintLine($"Попадание! +{delta} к общему счету.", ConsoleColor.Green);
        }

        ConsoleUtils.PrintLine($"Общий счет: {score.Total}", ConsoleColor.White);
        Console.WriteLine();
    }

    /// <summary>
    /// Прощается с игроков.
    /// </summary>
    /// <param name="score">Общее количесто очков.</param>
    public static void SayGoodBye(IScore score)
    {
        Console.Clear();
        ConsoleUtils.PrintLine($"Ваш счет: {score.Total}. Заходите еще!", ConsoleColor.White);
        Console.ReadKey();
    }
}
