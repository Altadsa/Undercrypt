using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArmouredSkeletonCombat : EnemyBaseState
{
    private float _attackSpeed = 3, _timeSinceAttack = 0;


    private bool CanAttack => _timeSinceAttack >= _attackSpeed;
    public ArmouredSkeletonCombat(Enemy enemy) : base(enemy)
    {
    }


    public override Type UpdateState()
    {
        if (!_enemy.Player) return typeof(ArmouredSkeletonChase);
        _timeSinceAttack += Time.deltaTime;
        if (!_player)
            _player = _enemy.Player;

        _transform.LookAt(_player, Vector3.up);

        if (InAttackRange)
        {

            if (CanAttack)
            {
                _timeSinceAttack = 0;

                return typeof(ArmouredSkeletonAttack);
            }
            return typeof(ArmouredSkeletonManoeuvre);
        }

        if (CanAttack)
        {
            _timeSinceAttack = 0;
            return typeof(ArmouredSkeletonLeap);
        }
        return typeof(ArmouredSkeletonChase);



    }



}

public class ArmouredSkeletonAttack : EnemyBaseState
{
    public ArmouredSkeletonAttack(Enemy enemy) : base(enemy)
    {
    }

    public override Type UpdateState()
    {
        _agent.SetDestination(_transform.position);
        _animator.SetTrigger("Attack");
        return typeof(ArmouredSkeletonCombat);
    }
}

public class ArmouredSkeletonLeap : EnemyBaseState
{
    public ArmouredSkeletonLeap(Enemy enemy) : base(enemy)
    {
    }

    public override Type UpdateState()
    {
        _agent.SetDestination(_transform.position);
        _animator.SetTrigger("Leap Attack");
        return typeof(ArmouredSkeletonCombat);
    }
}

public class ArmouredSkeletonManoeuvre : EnemyBaseState
{
    public ArmouredSkeletonManoeuvre(Enemy enemy) : base(enemy)
    {
    }

    public override Type UpdateState()
    {
        if (!_agent.hasPath)
        {
            _transform.LookAt(_player, Vector3.up);
            CirclePlayer();
        }

        return typeof(ArmouredSkeletonCombat);
    }

    private void CirclePlayer()
    {
        float rot = Random.Range(0, 2) > 0 ? 1 : -1;
        Vector3 newCurrent =
            _transform.position + (_transform.forward * _combatRange + _transform.right * _combatRange * rot);
        Debug.DrawRay(newCurrent, Vector3.up,Color.blue);
        _agent.SetDestination(newCurrent);
    }
}