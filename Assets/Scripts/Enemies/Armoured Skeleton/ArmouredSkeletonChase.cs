using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class ArmouredSkeletonChase : EnemyBaseState
{
    private float _combatRange = 2;
    public ArmouredSkeletonChase(Enemy enemy) : base(enemy)
    {

    }

    public override Type UpdateState()
    {
        _animator.SetBool("Moving", _agent.hasPath);
        _transform.LookAt(_player, Vector3.up);
        if (!_enemy.Player)
        {
            _enemy.SetPlayer(Object.FindObjectOfType<PlayerController>().transform);
            _player = _enemy.Player;
        }
        else
        {
            _agent.SetDestination(_player.position);
            if (InAttackRange)
            {
                _agent.SetDestination(_transform.position);
                return typeof(ArmouredSkeletonCombat);
            }
        }

        return typeof(ArmouredSkeletonChase);
    }
}

