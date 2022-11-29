using System;
using System.Threading.Tasks;

namespace TargetGame.Domain;

public sealed class Game
{
    private readonly Target _target;
    private readonly Score _score;
    private readonly Player _player;
    private readonly IUserInput _input;

    internal Game(Target target, Score score, Player player, IUserInput input)
    {
        _target = target;
        _score = score;
        _player = player;
        _input = input;
    }

    public IScore Score => _score;

    public event Action? OnStart;
    public event Action? OnUpdate;
    public event Action? OnTerminate;

    /// <summary>
    /// Вызвается первым при запуске игры.
    /// Ответственен за настройку параметров игры.
    /// </summary>
    public void Start()
    {
        _player.Shot += OnPlayerShot;

        OnStart?.Invoke();
    }


    /// <summary>
    /// Основной цикл игры.
    /// </summary>
    public async Task UpdateAsync()
    {
        while (true)
        {
            await _player.ShootAsync(_target);

            bool wantsToTerminate = _input.AskForTermination();
            if (wantsToTerminate) break;

            OnUpdate?.Invoke();
        }
    }

    /// <summary>
    /// Вызывается перед выходом из игры.
    /// </summary>
    public void Terminate()
    {
        _player.Shot -= OnPlayerShot;

        OnTerminate?.Invoke();
    }

    private void OnPlayerShot(Point hit) => _score.Append(_target, hit);
}
