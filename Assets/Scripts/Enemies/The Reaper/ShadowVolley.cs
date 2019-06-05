using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class ShadowVolley : BossBaseState
{
    private float _projectileSpawnRate = 0.2f, _timeSinceLastSpawn = 0;
    private int _projectileCount = 0, _projectilesToSpawn = 10;

    private readonly GameObject _projectilePrefab;
    public ShadowVolley(Enemy enemy, EnemyHealth bossHealth, GameObject projectilePrefab) : base(enemy, bossHealth)
    {
        _projectilePrefab = projectilePrefab;
    }

    public override Type UpdateState()
    {
        while (_projectileCount < _projectilesToSpawn)
        {
            while (_timeSinceLastSpawn < _projectileSpawnRate)
            {
                _timeSinceLastSpawn += Time.deltaTime;
                return typeof(ShadowVolley);
            }

            _timeSinceLastSpawn = 0;
            GameObject proj = Object.Instantiate(_projectilePrefab, _transform.position  + 2 * Vector3.up, Quaternion.identity, _enemy.AreasOfEffect);
            proj.transform.forward = RandomDirection;
            _projectileCount++;
        }
        _projectileCount = 0;
        return typeof(ReaperPhase3);
    }

    private Vector3 RandomDirection => new Vector3(Random.Range(-1, 1f), Random.Range(0.1f, 0.8f), Random.Range(-1, 1f)).normalized;
}