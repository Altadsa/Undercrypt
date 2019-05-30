using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class StationaryState : EnemyBaseState
{

    private float _stateDuration = 2f, _timeStationary = 0f;
    private readonly Vector3 _enemySpawn;

    private Vector3 randomDestination => new Vector3(_enemySpawn.x + Random.Range(-5f, 5f), 0,
        _enemySpawn.z + Random.Range(-5f, 5f));

    public StationaryState(Enemy enemy) : base(enemy)
    {
        _enemySpawn = _transform.position;
    }

    public override Type UpdateState()
    {
        _timeStationary += Time.deltaTime;

        var potentialPlayer = _enemy.Vision.UpdateVision();
        if (potentialPlayer)
        {
            _enemy.SetPlayer(potentialPlayer);
            //_animator.SetTrigger("Alerted");
            //_animator.SetBool("HasTarget", true);
            return typeof(ChaseState);
        }
        if (_timeStationary >= _stateDuration)
        {
            _timeStationary = 0;
            while (!_agent.hasPath)
            {
                NavMeshPath path = new NavMeshPath();
                if (_agent.CalculatePath(randomDestination, path))
                {
                    _agent.SetPath(path);
                    return typeof(WanderState);
                }
            }
        }

        return typeof(StationaryState);
    }
}