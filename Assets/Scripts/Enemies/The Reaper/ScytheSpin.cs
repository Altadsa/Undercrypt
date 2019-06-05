using System;
using UnityEngine;

public class ScytheSpin : BossBaseState
{
    private float _spinTime = 5f, _timeSpinning = 0;
    private Vector3 _originalPosition;
    public ScytheSpin(Enemy enemy, EnemyHealth bossHealth) : base(enemy, bossHealth)
    {
        _originalPosition = _transform.position;
    }

    public override Type UpdateState()
    {
        //Set Animator To Spin Attack
        while (_timeSpinning < _spinTime)
        {
            _timeSpinning += Time.deltaTime;
            _agent.SetDestination(_player.position);
            return typeof(ScytheSpin);
        }

        _timeSpinning = 0;
        _agent.SetDestination(_originalPosition);
        return typeof(ReaperPhase2);
    }
}