using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class ScytheAoE : BossBaseState
{
    private float _timeToSpawn = 3, _timerCd = 0;
    private GameObject _aoePrefab;
    public ScytheAoE(Enemy enemy, EnemyHealth bossHealth, GameObject aoePrefab) : base(enemy, bossHealth)
    {
        _aoePrefab = aoePrefab;
    }

    public override Type UpdateState()
    {
        _transform.LookAt(_player, Vector3.up);
        if (_timerCd >= _timeToSpawn)
        {
            _timerCd = 0;
            _animator.SetTrigger("Slash");
            Object.Instantiate(_aoePrefab, _transform.position,
                Quaternion.identity, _enemy.AreasOfEffect).transform.forward = _transform.forward;
            return _bossHealth.HealthRemaining > 0.66f ? typeof(ReaperPhase1) : typeof(ReaperPhase2);
        }

        _timerCd += Time.deltaTime;
        return typeof(ScytheAoE);
    }
}