using System;

public class WanderState : EnemyBaseState
{
    public WanderState(Enemy enemy) : base(enemy)
    {
    }

    public override Type UpdateState()
    {
        if (!_agent.hasPath)
        {
            return typeof(StationaryState);
        }

        return typeof(WanderState);
    }
}