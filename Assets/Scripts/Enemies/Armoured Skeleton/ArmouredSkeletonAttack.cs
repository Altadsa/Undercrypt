using System;

public class ArmouredSkeletonAttack : EnemyBaseState
{
    public ArmouredSkeletonAttack(Enemy enemy) : base(enemy)
    {
    }

    public override Type UpdateState()
    {
        _agent.SetDestination(_transform.position);
        _animator.SetTrigger("Attack");
        return typeof(ArmouredSkeletonCombat);
    }
}