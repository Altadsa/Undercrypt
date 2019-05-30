using System;

public class WanderState : EnemyBaseState
{
    public WanderState(Enemy enemy) : base(enemy)
    {
    }

    public override Type UpdateState()
    {
        var potentialPlayer = _enemy.Vision.UpdateVision();
        if (potentialPlayer)
        {
            _enemy.SetPlayer(potentialPlayer);
            //_animator.SetBool("HasTarget", true);
            return typeof(ChaseState);
        }
        if (!_agent.hasPath)
        {
            return typeof(StationaryState);
        }

        return typeof(WanderState);
    }
}