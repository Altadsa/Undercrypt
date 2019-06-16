using System.Collections;
using UnityEngine;

public class PlayerJump : ICommandInput
{
    Rigidbody _playerRb;
    Animator _playerAnimator;
    Transform _groundCheck;
    float _jumpHeight = 15;
    float _groundDistance = 0.2f;
    int _groundLayer = LayerMask.GetMask("Ground");

    private bool _isGrounded => Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundLayer);

    public PlayerJump(Rigidbody playerRb, Animator playerAnimator, Transform groundCheck, float jumpPower)
    {
        _playerRb = playerRb;
        _playerAnimator = playerAnimator;
        _groundCheck = groundCheck;
        _jumpHeight = jumpPower;
    }

    public bool CommandKey => Input.GetButtonDown("Jump");

    public void Update()
    {
        if (CommandKey && _isGrounded)
        { 
            _playerAnimator.SetTrigger("Jump");
            _playerRb.velocity = new Vector3(_playerRb.velocity.x, _jumpHeight, _playerRb.velocity.z);
        }

    }
}

