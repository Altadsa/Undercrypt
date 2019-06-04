using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CameraSettings _settings;
    [SerializeField] Transform _playerTransform;
    [SerializeField] private Transform _lockOnTarget;

    Camera _mainCamera;
    IAxisInput _cameraAxisInput;
    CameraRotator _cameraRotator;
    CameraCollision _cameraCollision;
    CameraZoom _cameraZoom;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _cameraAxisInput = _settings.AxisSettings();
        _cameraRotator = _settings.RotationSettings(_cameraAxisInput, transform);
        _cameraCollision = _settings.CollisionSettings(_mainCamera.transform, transform);
        _cameraZoom = _settings.ZoomSettings(_mainCamera.transform);
        //_cameraRotator.LockTarget(_lockOnTarget);
    }

    void Update()
    {
        _cameraAxisInput?.ReadInput();
        _cameraRotator?.RotateCamera();
        _cameraCollision?.CheckForCameraCollision();
        _cameraZoom?.Update();
    }

    private void LateUpdate()
    {
        transform.position = _playerTransform.position + Vector3.up;
    }



}