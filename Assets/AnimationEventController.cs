using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventController : MonoBehaviour
{
    [SerializeField] private CapsuleCollider _body;
    [SerializeField] private BoxCollider _weapon;

    public void StartAttack()
    {
        _weapon.enabled = true;
    }

    public void EndAttack()
    {
        _weapon.enabled = false;
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
