using System;
using UnityEngine;

public class ShadowWalls : BossBaseState
{
    private float _gatherTime = 3;
    public ShadowWalls(Enemy enemy, EnemyHealth bossHealth) : base(enemy, bossHealth)
    {
    }

    public override Type UpdateState()
    {
        GameObject[] ojbs = new GameObject[_enemy.AreasOfEffect.childCount];
        for (int i = 1; i < ojbs.Length; i++)
        {
            ojbs[i] = _enemy.AreasOfEffect.GetChild(i).gameObject;
        }

        foreach (var gameObject in ojbs)
        {
            gameObject.SetActive(false);
        }

        return typeof(ReaperPhase3);
    }
}