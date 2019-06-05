using System;
using UnityEngine;

public class ReaperPhase2 : BossBaseState
{
    private const float PHASE_CHANGE_VALUE = 0.33f;

    public ReaperPhase2(Enemy enemy, EnemyHealth bossHealth) : base(enemy, bossHealth)
    {
    }

    public override Type UpdateState()
    {
        Debug.Log("Phase 2 Yay");
        if (_bossHealth.HealthRemaining <= PHASE_CHANGE_VALUE)
        {
            return typeof(ReaperPhase3);
        }

        return _enemy.AreasOfEffect.childCount < 4 
            ? typeof(ScytheAoE) 
            : typeof(ScytheSpin);
    }
}