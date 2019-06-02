using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _zoomSpeed;
    [SerializeField] Transform _playerTransform;
    [SerializeField] bool _joystickEnabled = false;

    [SerializeField] private Transform _lockOnTarget;


    IAxisInput _cameraAxisInput;
    
    Camera _mainCamera;

    CameraRotator _cameraRotator;
    CameraCollision _cameraCollision;
    CameraZoom _cameraZoom;

    private void Awake()
    {
        _cameraAxisInput = _joystickEnabled
            ? _cameraAxisInput = new JoystickCameraAxisInput()
            : _cameraAxisInput = new MouseCameraAxisInput();
        _mainCamera = Camera.main;
        _cameraRotator = new CameraRotator(_cameraAxisInput, transform);
        _cameraCollision = new CameraCollision(_mainCamera.transform, transform);
        _cameraRotator.LockTarget(_lockOnTarget);
    }

    void Update()
    {
        _cameraAxisInput.ReadInput();
        _cameraRotator.RotateCamera();
        _cameraCollision.CheckForCameraCollision();
    }

    private void LateUpdate()
    {
        transform.position = _playerTransform.position + Vector3.up;
    }

}