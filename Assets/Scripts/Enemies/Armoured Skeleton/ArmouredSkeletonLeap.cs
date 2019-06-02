using System;

public class ArmouredSkeletonLeap : EnemyBaseState
{
    public ArmouredSkeletonLeap(Enemy enemy) : base(enemy)
    {
    }

    public override Type UpdateState()
    {
        _agent.SetDestination(_transform.position);
        _animator.SetTrigger("Leap Attack");
        return typeof(ArmouredSkeletonCombat);
    }
}