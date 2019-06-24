using Platformer_3D.Extensions;
using UnityEngine;

public class PlayerMovement
{
    private Camera _mCamera;
    private IAxisInput _input;
    private Rigidbody _playerRb;
    private Transform _playerTransform;
    private Animator _playerAnimator;
    private float _movementSpeed = 2500f;

    private Vector3 ForwardMovement =>
        _playerTransform.forward * _input.Vertical + _playerTransform.right * _input.Horizontal;
    private Vector3 CameraMovement => 
        _mCamera.ScaledForward() * _input.Vertical + _mCamera.ScaledRight() * _input.Horizontal;

    public PlayerMovement(IAxisInput input, Rigidbody playerRb, Animator playerAnimator, float speed)
    {
        _mCamera = Camera.main;
        _input = input;
        _playerRb = playerRb;
        _playerTransform = playerRb.transform;
        _playerAnimator = playerAnimator;
        _movementSpeed = speed;
    }

    public void Update()
    {
        _playerAnimator.SetFloat("WalkForce", Mathf.Abs(CameraMovement.x) + Mathf.Abs(CameraMovement.z));
        UpdateDirection();
        if (Z_Targeting.PlayerTarget == null)
        {
            _playerAnimator.SetBool("HasTarget", false);
            _playerTransform.position += CameraMovement * _movementSpeed * Time.deltaTime;
        }
        else
        {
            _playerAnimator.SetBool("HasTarget", true);
            _playerTransform.position += ForwardMovement * _movementSpeed * Time.deltaTime;
        }

    }

    public void Move()
    {
       // _playerRb.MovePosition(_playerRb.position + CameraMovement * _movementSpeed * Time.fixedDeltaTime);
       //_playerRb.velocity = (CameraMovement * _movementSpeed * Time.fixedDeltaTime) + Physics.gravity;
    }

    private void UpdateDirection()
    {
        if (Z_Targeting.PlayerTarget == null)
        {
            var newForward = _input.HasAxisInput ? CameraMovement.normalized : _playerTransform.forward;
            var angle = Vector3.Angle(_playerTransform.forward, newForward);
            _playerTransform.forward = Vector3.Lerp(_playerTransform.forward, newForward, Time.deltaTime * angle);
        }
        else
        {
            var dir = Z_Targeting.PlayerTarget.Transform.position - _playerTransform.position;
            dir.y = 0;
            _playerTransform.forward = dir.normalized;
        }

    }

}