using System;
using UnityEngine;

public class ReaperPhase3 : BossBaseState
{
    public ReaperPhase3(Enemy enemy, EnemyHealth bossHealth) : base(enemy, bossHealth)
    {
    }

    public override Type UpdateState()
    {
        return _enemy.AreasOfEffect.childCount < 11
            ? typeof(ShadowVolley)
            : typeof(ShadowWalls);
    }
}