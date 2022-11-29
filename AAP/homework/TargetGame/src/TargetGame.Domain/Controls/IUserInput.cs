namespace TargetGame.Domain.Controls;

public interface IUserInput
{
    /// <summary>
    /// </summary>
    /// <returns>Перестал ли прицеливаться игрок</returns>
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
