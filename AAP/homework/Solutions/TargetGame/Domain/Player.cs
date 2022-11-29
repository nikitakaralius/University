using System;
using System.Threading.Tasks;

namespace TargetGame.Domain;

public sealed class Player
{
    private readonly IWeapon _weapon;

    public Player(IWeapon weapon)
    {
        _weapon = weapon;
    }

    public event Action<Point>? Shot;

    /// <summary>
    /// Производит выстрел в выбранную позицию.
    /// </summary>
    /// <param name="target">Мишень, по которой стреляет игрок.</param>
    /// <returns>Точка на мишени, куда попал игрок.</returns>
    public async Task ShootAsync(Target target)
    {
        var position = await _weapon.ShootAsync(target);
        Shot?.Invoke(position);
    }
}
