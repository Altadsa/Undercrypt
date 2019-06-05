﻿using System;
using UnityEngine;

public class ScytheAttack : BossBaseState
{
    private float _timeStuck = 0, _maxStickingTime = 3;
    private Vector3 _originalPosition;

    private Vector3 _attackPosition;
    private float _normalSpeed, _attackSpeed = 30;

    private bool _attackFinished = false, _destinationSet = false;
    public ScytheAttack(Enemy enemy, EnemyHealth bossHealth) : base(enemy, bossHealth)
    {
        _originalPosition = _transform.position;
    }

    public override Type UpdateState()
    {
        //_transform.forward = Vector3.Scale((_player.position - _transform.position).normalized, new Vector3(1, 0, 1));
        if (!_destinationSet)
        {
            _attackPosition = _player.position;
            _normalSpeed = _agent.acceleration;
            _agent.acceleration = _attackSpeed;
            _agent.SetDestination(_attackPosition);
            _destinationSet = true;
        }

        if (_destinationSet)
        {
            if (!_agent.hasPath)
            {
                _agent.acceleration = _normalSpeed;
                _attackFinished = true;
            }
        }
        if (!_attackFinished) return typeof(ScytheAttack);

        _timeStuck += Time.deltaTime;
        if (_timeStuck >= _maxStickingTime)
        {
            _timeStuck = 0;
            _agent.SetDestination(_originalPosition);
            _attackFinished = false;
            _destinationSet = false;
            return typeof(ReaperPhase1);
        }


        return typeof(ScytheAttack);
    }

}