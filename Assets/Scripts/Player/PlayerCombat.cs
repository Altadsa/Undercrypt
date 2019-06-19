using UnityEngine;

public class PlayerCombat
{
    private Transform _swordParent;
    private PlayerSword _playerSword;
    private Animator _animator;

    public PlayerCombat(Transform swordParent, Animator animator)
    {
        _swordParent = swordParent;
        _animator = animator;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Inventory.Instance.EquipItem(Inventory.Instance.EquippedWeapon.ItemId);
            _playerSword = _swordParent.GetComponentInChildren<PlayerSword>();
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