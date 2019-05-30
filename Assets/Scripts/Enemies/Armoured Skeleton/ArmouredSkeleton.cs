using System.Collections.Generic;

public class ArmouredSkeleton : Enemy
{
    new void Awake()
    {
        base.Awake();
        var initialStates = new List<EnemyBaseState>()
        {
            new ArmouredSkeletonChase(this),
            new ArmouredSkeletonCombat(this)
        };
        StateMachine = new EnemyStateMachine(initialStates);
    }
}