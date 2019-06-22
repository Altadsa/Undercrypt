using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(DungeonDoor))]
public class BattleDoorCondition : MonoBehaviour, IDoorCondition
{
    private List<EnemyHealth> _enemies;

    public Component DoorCondition => this;

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

    public bool ConditionsMet()
    {
        return _enemies.Count == 0;
    }

    public void ConditionMessage()
    {
        Debug.Log("You need to defeat all enemies in the room.");
    }
}

public interface IDoorCondition
{
    bool ConditionsMet();
    void ConditionMessage();
    Component DoorCondition { get; }
}