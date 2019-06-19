using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventController : MonoBehaviour
{
    [SerializeField] private CapsuleCollider _body;
    [SerializeField] private Transform _weaponParent;

    Collider _weaponCollider;

    public void StartAttack()
    {
        _weaponCollider = _weaponParent.GetComponentInChildren<Collider>();
        _weaponCollider.enabled = true;
    }

    public void EndAttack()
    {
        _weaponCollider = _weaponParent.GetComponentInChildren<Collider>();
        _weaponCollider.enabled = false;
    }

    public void StartJump()
    {
        _body.enabled = true;
    }

    public void EndJump()
    {
        _body.enabled = false;
    }
}
