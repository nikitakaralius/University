using System.Threading.Tasks;

namespace TargetGame.Domain;

public interface IWeapon
{
    /// <summary>
    /// Производит выстрел в выбранную позицию.
    /// </summary>
    /// <param name="target">Мишень, по которой стреляет игрок.</param>
    /// <returns>Точка на мишени, куда попал игрок.</returns>
    Task<Point> ShootAsync(Target target);
}
