using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArmouredSkeletonManoeuvre : EnemyBaseState
{
    public ArmouredSkeletonManoeuvre(Enemy enemy) : base(enemy)
    {
    }

    public override Type UpdateState()
    {
        if (!_agent.hasPath)
        {
            _transform.LookAt(_player, Vector3.up);
            CirclePlayer();
        }

        return typeof(ArmouredSkeletonCombat);
    }

    private void CirclePlayer()
    {
        float rot = Random.Range(0, 2) > 0 ? 1 : -1;
        Vector3 newCurrent =
            _transform.position + (_transform.forward * _combatRange + _transform.right * _combatRange * rot);
        Debug.DrawRay(newCurrent, Vector3.up,Color.blue);
        _agent.SetDestination(newCurrent);
    }
}