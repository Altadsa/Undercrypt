using System;
using UnityEngine;

public class ChaseState : EnemyBaseState
{
    public ChaseState(Enemy enemy) : base(enemy)
    {
    }

    public override Type UpdateState()
    {
        var distToPlayer = Vector3.Distance(_enemy.Player.position, _transform.position);
        RaycastHit hit;
        bool inSight = Physics.Raycast(_transform.position,
            (_enemy.Player.position - _transform.position).normalized, out hit, 20);
        if (inSight)
        {
            if (hit.collider.GetComponent<PlayerController>())
            {
                _agent.SetDestination(_enemy.Player.position);
                if (distToPlayer < 5f)
                {
                    //_animator.SetBool("InRange", true);
                    Debug.Log("Within Attacking Range");
                    return typeof(CombatState);
                }

                Debug.Log("Chasing Player.");
                return typeof(ChaseState);
            }

        }
        Debug.Log("Player out of sight. Moving to Last known Location");
        _enemy.Alert(_enemy.Player.position);
        return typeof(AlertState);
    }
}