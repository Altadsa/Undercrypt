using Platformer_3D.Extensions;
using UnityEngine;

public class PlayerMovement
{
    private Camera _mCamera;
    private IAxisInput _input;
    private Rigidbody _playerRb;
    private Transform _playerTransform;
    private Animator _playerAnimator;
    private float _movementSpeed = 5f;

    private Vector3 FreeMovement =>
        _playerTransform.forward * _input.Vertical + _playerTransform.right * _input.Horizontal;
    private Vector3 CameraMovement => 
        _mCamera.ScaledForward() * _input.Vertical + _mCamera.ScaledRight() * _input.Horizontal;
    private bool RotationButtonsPressed => Input.GetMouseButton(1) && !Input.GetMouseButton(0);

    public PlayerMovement(IAxisInput input, Rigidbody playerRb, Animator playerAnimator)
    {
        _mCamera = Camera.main;
        _input = input;
        _playerRb = playerRb;
        _playerTransform = playerRb.transform;
        _playerAnimator = playerAnimator;
    }

    public void Update()
    {

        _playerAnimator.SetFloat("WalkForce", Mathf.Abs(CameraMovement.x) + Mathf.Abs(CameraMovement.z));
    }

    public void Move()
    {
        _playerRb.MovePosition(_playerRb.position + CameraMovement * _movementSpeed * Time.fixedDeltaTime);
    }

    public void UpdatePlayerDirection()
    {
        _playerTransform.forward = _input.HasAxisInput ? CameraMovement.normalized : _playerTransform.forward;
    }

}