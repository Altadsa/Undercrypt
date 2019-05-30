using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public abstract class EnemyBaseState
{
    protected Enemy _enemy;
    protected Transform _transform;
    protected Animator _animator;
    protected NavMeshAgent _agent;

    protected EnemyBaseState(Enemy enemy)
    {
        _enemy = enemy;
        _transform = enemy.transform;
        //_animator = enemy.GetComponent<Animator>();
        _agent = enemy.GetComponent<NavMeshAgent>();
    }

    public abstract Type UpdateState();
}