using System;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class ArmouredSkeletonCombat : EnemyBaseState
{
    private int _damage = 4;
    private float _attackSpeed = 4, _timeSinceAttack = 0;
    private float _combatRange = 3;

    private float _manoeuvreTime = 3, _timeSinceLastManoeuvre = 0;
    public ArmouredSkeletonCombat(Enemy enemy) : base(enemy)
    {
        _animator = enemy.GetComponentInChildren<Animator>();
    }

    public override Type UpdateState()
    {
        UpdateTimers();
        if (!_isJumping)
        {
            _transform.LookAt(_enemy.Player, Vector3.up);
        }
        else
        {
            if (!_agent.hasPath)
                _isJumping = false;
        }
        if (!_enemy.Player) return typeof(ArmouredSkeletonChase);
        if (_timeSinceAttack > _attackSpeed && !_agent.hasPath)
        {
            _timeSinceAttack = 0;
            _enemy.Player.GetComponent<PlayerHealth>().UpdateHealth(-_damage);
        }
        else if (_timeSinceLastManoeuvre > _manoeuvreTime)
        {
            _timeSinceLastManoeuvre = 0;
            DetermineCombatManoeuvre();
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

    private void DetermineCombatManoeuvre()
    {
        int choice = Random.Range(0, 2);
        if (choice > 0)
            JumpBehindPlayer();
        else
            CirclePlayer();    
    }

    private bool _isJumping = false;
    private void JumpBehindPlayer()
    {
        Vector3 newPosition = _transform.position + _transform.forward * 2 * _combatRange;
        Debug.DrawRay(newPosition, -_transform.forward);
        _agent.speed = 60;
        _agent.SetDestination(newPosition);
        _animator.SetTrigger("Jump");
        _isJumping = true;
        _agent.speed = 3.5f;
    }

    private void CirclePlayer()
    {
        float rot = Random.Range(0, 2) > 0 ? 1 : -1;
        Vector3 newCurrent =
            _transform.position + (_transform.forward * _combatRange + _transform.right * _combatRange * rot);
        _agent.SetDestination(newCurrent);
    }
}