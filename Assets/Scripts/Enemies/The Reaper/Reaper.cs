using System.Collections.Generic;
using UnityEngine;

public class Reaper : Enemy
{
    [SerializeField] private GameObject _aoePrefab;
    [SerializeField] private EnemyAoE _shadowBarrier;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private EnemyHealth _health;

    protected new void Awake()
    {
        base.Awake();
        SetPlayer(FindObjectOfType<PlayerHealth>().transform);
        var initialStates = new List<EnemyBaseState>()
        {
            new ReaperPhase1(this, _health),
            new ScytheAoE(this, _health, _aoePrefab),
            new ScytheAttack(this, _health),
            new ReaperPhase2(this, _health),
            new ScytheSpin(this, _health),
            new ReaperPhase3(this, _health),
            new ShadowVolley(this, _health, _projectile),
            new ShadowWalls(this, _health)
        };
        StateMachine = new EnemyStateMachine(initialStates);
    }

}