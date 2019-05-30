using System.Collections.Generic;

public class Skeleton : Enemy
{
    new void Awake()
    {
        base.Awake();
        var initialStates = new List<EnemyBaseState>()
        {
            new StationaryState(this),
            new WanderState(this),
            new ChaseState(this),
            new CombatState(this),
            new AlertState(this)
        };
        StateMachine = new EnemyStateMachine(initialStates);
    }

}