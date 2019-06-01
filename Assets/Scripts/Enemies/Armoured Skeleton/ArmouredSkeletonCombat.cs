using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArmouredSkeletonCombat : EnemyBaseState
{
    private int _damage = 4;
    private float _attackSpeed = 4, _timeSinceAttack = 0;
    private float _combatRange = 2;

    private float _manoeuvreTime = 3, _timeSinceLastManoeuvre = 0;
    public ArmouredSkeletonCombat(Enemy enemy) : base(enemy)
    {
    }

    public override Type UpdateState()
    {
        UpdateTimers();
        _transform.LookAt(_enemy.Player, Vector3.up);
        if (!_enemy.Player) return typeof(ArmouredSkeletonChase);
        if (_timeSinceAttack > _attackSpeed)
        {
            _timeSinceAttack = 0;
            _animator.SetTrigger("Attack");
        }
        else if (_timeSinceLastManoeuvre > _manoeuvreTime)
        {
            _timeSinceLastManoeuvre = 0;
            CirclePlayer();



        }
        else
        {
            _animator.SetBool("Moving", _agent.hasPath);
        }


        if (Vector3.Distance(_transform.position, _enemy.Player.position) > _combatRange)
        {
            return typeof(ArmouredSkeletonChase);
        }
        return typeof(ArmouredSkeletonCombat);

    }

    private void UpdateTimers()
    {
        _timeSinceAttack += Time.deltaTime;
        _timeSinceLastManoeuvre += Time.deltaTime;
    }

    private void CirclePlayer()
    {
        float rot = Random.Range(0, 2) > 0 ? 1 : -1;
        Vector3 newCurrent =
            _transform.position + (_transform.forward * _combatRange + _transform.right * _combatRange * rot);
        _agent.SetDestination(newCurrent);
    }
}