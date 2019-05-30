using System;
using UnityEngine;

public class ArmouredSkeletonCombat : EnemyBaseState
{

    private float _attackSpeed = 4, _timeSinceAttack = 0;
    public ArmouredSkeletonCombat(Enemy enemy) : base(enemy)
    {
    }

    public override Type UpdateState()
    {
        _timeSinceAttack += Time.deltaTime; 
        if (_enemy.Player)
        {
            if (_timeSinceAttack > _attackSpeed)
            {
                _timeSinceAttack = 0;
                Debug.Log("Attacking");
                _enemy.Player.GetComponent<PlayerHealth>().UpdateHealth(-4);
            }

            return typeof(ArmouredSkeletonCombat);
        }

        return typeof(ArmouredSkeletonChase);

    }
}