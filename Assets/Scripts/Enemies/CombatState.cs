using System;
using UnityEngine;

public class CombatState : EnemyBaseState
{
    float combatRange = 5f, _attackRate = 2.5f, _timeSinceAttack = 0;
    public CombatState(Enemy enemy) : base(enemy)
    {
    }

    public override Type UpdateState()
    {
        if (_enemy.Player && isWithinAttackRange)
        {
            var playerHealth = _enemy.Player.GetComponent<PlayerHealth>();
            SetCombatMovement();
            _timeSinceAttack += Time.deltaTime;
            if (_timeSinceAttack >= _attackRate)
            {
                _timeSinceAttack = 0;
                playerHealth?.UpdateHealth(-2);
                //_animator.SetTrigger("Attack");
                Debug.Log("Attack.");
            }

            Debug.Log("Attack on Cooldown.");
            return typeof(CombatState);
        }

        //_animator.SetBool("InRange", false);
        //Debug.Log("Player out of Attack Range. Chasing Player.");
        _timeSinceAttack = 0;
        _agent.isStopped = false;
        return typeof(ChaseState);
    }

    void SetCombatMovement()
    {
        _agent.isStopped = true;
        _transform.LookAt(_enemy.Player, Vector3.up);
    }

    bool isWithinAttackRange => Vector3.Distance(_transform.position, _enemy.Player.position) <= combatRange;
}