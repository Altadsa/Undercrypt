using UnityEngine;

[CreateAssetMenu (menuName = "Scriptable Objects/Camera Settings")]
public class CameraSettings : ScriptableObject
{
    [SerializeField] private float _zoomSpeed = 10;
    [SerializeField] private bool _joystickEnabled = false;
    [SerializeField] private bool _canZoom = false;
    [SerializeField] private bool _canRotate = false;
    [SerializeField] private bool _collisionEnabled = false;

    public IAxisInput AxisSettings()
    {
        return _joystickEnabled
            ?  (IAxisInput) new JoystickCameraAxisInput()
            :  new MouseCameraAxisInput();
    }

    public CameraRotator RotationSettings(IAxisInput axisInput, Transform controller)
    {
        return _canRotate
            ? new CameraRotator(axisInput, controller)
            : null;
    }

    public CameraZoom ZoomSettings(Transform mainCamera)
    {
        return _canZoom
            ? new CameraZoom(mainCamera, _zoomSpeed)
            : null;
    }

    public CameraCollision CollisionSettings(Transform camera, Transform controller)
    {
        return _collisionEnabled
            ? new CameraCollision(camera, controller)
            : null;
    }
}