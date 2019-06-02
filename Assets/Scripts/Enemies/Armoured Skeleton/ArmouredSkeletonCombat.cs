using System;
using UnityEngine;

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

public class ArmouredSkeletonDodge : EnemyBaseState
{
    public ArmouredSkeletonDodge(Enemy enemy) : base(enemy)
    {
    }

    public override Type UpdateState()
    {
        return typeof(ArmouredSkeletonCombat);
    }
}