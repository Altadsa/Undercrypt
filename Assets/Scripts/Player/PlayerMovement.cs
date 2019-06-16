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

    private Vector3 FreeMovement =>
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
        _playerTransform.position += CameraMovement * _movementSpeed * Time.deltaTime;
    }

    public void Move()
    {
       // _playerRb.MovePosition(_playerRb.position + CameraMovement * _movementSpeed * Time.fixedDeltaTime);
       //_playerRb.velocity = (CameraMovement * _movementSpeed * Time.fixedDeltaTime) + Physics.gravity;
    }

    private void UpdateDirection()
    {
        _playerTransform.forward = _input.HasAxisInput ? CameraMovement.normalized : _playerTransform.forward;
    }

}