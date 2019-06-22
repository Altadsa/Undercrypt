using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(DungeonRoom))]
public class RoomBattle : MonoBehaviour
{
    [SerializeField] private GameObject _battleChest;
    [SerializeField] private GameObject _doorToLock;

    private List<EnemyHealth> _enemies;

    private DungeonRoom _dungeonRoom;

    private void OnEnable()
    {
        _dungeonRoom = GetComponent<DungeonRoom>();
        _doorToLock.AddComponent<BattleDoorCondition>();
        _enemies = _dungeonRoom.GetComponentsInChildren<EnemyHealth>().ToList();
        _enemies.ForEach(e => e.OnEnemyDeath += OnEnemyDeath);
        _doorToLock.GetComponent<BattleDoorCondition>().Initialize(_enemies);
    }

    private void OnEnemyDeath(EnemyHealth enemy)
    {
        if (_enemies.Contains(enemy))
        {
            _enemies.Remove(enemy);
        }

        if (_enemies.Count == 0)
        {
            _battleChest.SetActive(true);
        }
    }

}
