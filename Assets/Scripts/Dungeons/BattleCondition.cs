using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BattleCondition : OpenCondition
{
    private List<EnemyHealth> _enemies;

    public void Initialize(List<EnemyHealth> enemies)
    {
        _enemies = enemies;
        _enemies.ForEach(e => e.OnEnemyDeath += OnEnemyDead);
    }

    private void OnEnemyDead(EnemyHealth enemy)
    {
        if (_enemies.Contains(enemy))
        {
            _enemies.Remove(enemy);
            ConditionsMet();
        }
    }

    public override bool ConditionsMet()
    {
        return _enemies.Count == 0;
    }

    public override void ConditionMessage()
    {
        Debug.Log("You need to defeat all enemies in the room.");
    }
}