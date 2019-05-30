using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class ArmouredSkeletonChase : EnemyBaseState
{
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
            if (Vector3.Distance(_transform.position, _enemy.Player.position) < 2f)
            {

                Debug.Log("In Attacking Ranges");
                return typeof(ArmouredSkeletonCombat);
            }
        }

        return typeof(ArmouredSkeletonChase);
    }
}

