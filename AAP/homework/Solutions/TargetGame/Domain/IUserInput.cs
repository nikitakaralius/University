namespace TargetGame.Domain;

public interface IUserInput
{
    bool StopAiming();

    /// <summary>
    /// Спрашивает игрока хочет ли он продолжить.
    /// </summary>
    /// <returns>
    /// true - если игрок нажал на пробел.
    /// false - во всех остальных случаях.
    /// </returns>
    bool AskForTermination();
}
