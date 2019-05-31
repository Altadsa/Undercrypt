using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class ArmouredSkeletonChase : EnemyBaseState
{
    private float _combatRange = 3;
    public ArmouredSkeletonChase(Enemy enemy) : base(enemy)
    {
    }

    public override Type UpdateState()
    {
        if (!_enemy.Player)
        {
            _enemy.SetPlayer(Object.FindObjectOfType<PlayerController>().transform);
        }
        else
        {
            _agent.SetDestination(_enemy.Player.position);
            if (Vector3.Distance(_transform.position, _enemy.Player.position) <= _combatRange)
            {
                _agent.SetDestination(_transform.position);
                Debug.Log("In Attacking Ranges");
                return typeof(ArmouredSkeletonCombat);
            }
        }

        return typeof(ArmouredSkeletonChase);
    }
}

