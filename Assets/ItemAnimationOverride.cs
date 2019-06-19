using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimationOverride : MonoBehaviour
{
     static Animator _player = null;
     static RuntimeAnimatorController _playerDefault;
    [SerializeField] AnimatorOverrideController _itemOverride;


    private void OnEnable()
    {
        if (_player == null)
        {
            var p = FindObjectOfType<PlayerHealth>().transform.GetChild(1);
            _player = p.GetComponent<Animator>();
            _playerDefault = _player.runtimeAnimatorController;
        }

        if (_itemOverride != null)
            _player.runtimeAnimatorController = _itemOverride;
    }

    private void OnDisable()
    {
        _player.runtimeAnimatorController = _playerDefault;
    }
}
