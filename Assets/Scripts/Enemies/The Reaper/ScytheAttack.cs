using System;
using UnityEngine;

public class ScytheAttack : BossBaseState
{
    private float _timeStuck = 0, _maxStickingTime = 3;
    private Vector3 _originalPosition;

    private bool _attackFinished = false;
    public ScytheAttack(Enemy enemy, EnemyHealth bossHealth) : base(enemy, bossHealth)
    {
        _originalPosition = _transform.position;
    }

    public override Type UpdateState()
    {
        if (!_attackFinished)
        {
            _attackFinished = true;
            _transform.position = _enemy.Player.position;
        }

        _timeStuck += Time.deltaTime;
        if (_timeStuck >= _maxStickingTime)
        {
            _timeStuck = 0;
            _transform.position = _originalPosition;
            _attackFinished = false;
            return typeof(ReaperPhase1);
        }

        return typeof(ScytheAttack);
    }

}