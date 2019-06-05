using System;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

[Serializable]
public abstract class EnemyBaseState
{
    protected Enemy _enemy;
    protected Transform _transform;
    protected Animator _animator;
    protected NavMeshAgent _agent;
    protected Transform _player;

    protected float _combatRange;

    protected bool InAttackRange => Vector3.Distance(_transform.position, _player.position) <= _combatRange;
    protected EnemyBaseState(Enemy enemy)
    {
        _enemy = enemy;
        _transform = enemy.transform;
        _animator = enemy.GetComponentInChildren<Animator>();
        _agent = enemy.GetComponent<NavMeshAgent>();
        _combatRange = 3;
    }

    public abstract Type UpdateState();
}

[Serializable]
public abstract class BossBaseState : EnemyBaseState
{
    protected EnemyHealth _bossHealth;
    protected BossBaseState(Enemy enemy, EnemyHealth bossHealth) : base(enemy)
    {
        _bossHealth = bossHealth;
        _player = Object.FindObjectOfType<PlayerHealth>().transform;
    }
}