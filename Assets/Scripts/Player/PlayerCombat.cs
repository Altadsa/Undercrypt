using UnityEngine;

public class PlayerCombat
{
    private PlayerSword _playerSword;
    private Animator _animator;

    public PlayerCombat(PlayerSword sword, Animator animator)
    {
        _playerSword = sword;
        _animator = animator;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger("Attack");
            _playerSword.Attack();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            _animator.SetBool("Defensive", true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            _animator.SetBool("Defensive", false);
        }
    }
}