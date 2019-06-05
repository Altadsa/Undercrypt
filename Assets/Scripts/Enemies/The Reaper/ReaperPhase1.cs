using System;
using UnityEngine;

public class ReaperPhase1 : BossBaseState
{
    private const float PHASE_CHANGE_VALUE = 0.66f;

    public ReaperPhase1(Enemy enemy, EnemyHealth bossHealth) : base(enemy, bossHealth)
    {
    }

    public override Type UpdateState()
    {
        Debug.Log("Phase 1");
        if (_bossHealth.HealthRemaining <= PHASE_CHANGE_VALUE)
        {
            return typeof(ReaperPhase2);
        }

        return _enemy.AreasOfEffect.childCount < 4 
            ? typeof(ScytheAoE) 
            : typeof(ScytheAttack);
    }


}