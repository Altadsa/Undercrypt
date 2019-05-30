using System;
using UnityEngine;

public class AlertState : EnemyBaseState
{
    public AlertState(Enemy enemy) : base(enemy)
    {
    }

    public override Type UpdateState()
    {
        _agent.SetDestination(_enemy.PlayerLastLocation);
        var player = _enemy.Vision.UpdateVision();
        if (player)
        {
            _enemy.SetPlayer(player);
            //_animator.SetBool("HasTarget", true);
            return typeof(ChaseState);
        }
        if (!_agent.hasPath)
        {
            Debug.Log("At Last Known Position of Player. Do Search here.");
            _enemy.SetPlayer(null);
            _enemy.Alert(Vector3.zero);
            //_animator.SetBool("HasTarget", false);
            return typeof(WanderState);
        }

        return typeof(AlertState);
    }
}