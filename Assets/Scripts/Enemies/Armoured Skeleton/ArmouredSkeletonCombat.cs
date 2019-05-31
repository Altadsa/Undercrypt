using System;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class ArmouredSkeletonCombat : EnemyBaseState
{
    private int _damage = 4;
    private float _attackSpeed = 4, _timeSinceAttack = 0;
    private float _combatRange = 3;
    public ArmouredSkeletonCombat(Enemy enemy) : base(enemy)
    {
    }

    public override Type UpdateState()
    {
        _timeSinceAttack += Time.deltaTime;
        _transform.LookAt(_enemy.Player, Vector3.up);
        if (!_enemy.Player) return typeof(ArmouredSkeletonChase);
        if (!_agent.hasPath)
        {

            DetermineCombatMovement();
        }

        if (_timeSinceAttack > _attackSpeed)
        {
            _timeSinceAttack = 0;
            Debug.Log("Attacking");
            _enemy.Player.GetComponent<PlayerHealth>().UpdateHealth(-_damage);
        }

        if (Vector3.Distance(_transform.position, _enemy.Player.position) > _combatRange)
        {
            return typeof(ArmouredSkeletonChase);
        }
        return typeof(ArmouredSkeletonCombat);

    }

    private void DetermineCombatMovement()
    {
        float rot = Random.Range(0, 2) > 0 ? 1 : -1;
        Vector3 newCurrent = _transform.position + (_transform.forward * _combatRange + _transform.right * _combatRange * rot);
        _agent.SetDestination(newCurrent);
    }
}