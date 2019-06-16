using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CameraSettings _settings;
    [SerializeField] Transform _playerTransform;

    Camera _mainCamera;
    IAxisInput _cameraAxisInput;
    CameraRotator _cameraRotator;
    CameraCollision _cameraCollision;
    CameraZoom _cameraZoom;
    CameraTargeting _cameraTargeting;

    //private List<ITargetable> _potentialTargets;
    private ITargetable _lockOnTarget;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _cameraAxisInput = _settings.AxisSettings();
        _cameraRotator = _settings.RotationSettings(_cameraAxisInput, transform);
        _cameraCollision = _settings.CollisionSettings(_mainCamera.transform, transform);
        _cameraZoom = _settings.ZoomSettings(_mainCamera.transform);
        _cameraTargeting = new CameraTargeting(transform);
        //_cameraRotator.LockTarget(_lockOnTarget);
    }

    void Update()
    {
        _cameraAxisInput?.ReadInput();

        _cameraCollision?.CheckForCameraCollision();
        _cameraZoom?.Update();
        if (_lockOnTarget == null)
        {
            _cameraRotator?.RotateCamera(); 
        }
        else
        {
            if (transform.forward.y < 0)
                _cameraTargeting.Update(_lockOnTarget);
            else
                _lockOnTarget = null;
        }
    }

    private void LateUpdate()
    {
        transform.position = _playerTransform.position + Vector3.up;
    }

    private void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponent<ITargetable>();
        if (target != null)
        {
            var dir = (target.Transform.position - transform.position).normalized;
            Debug.DrawRay(transform.position, dir * 10, Color.cyan, 10);
            var inFront = Vector3.Dot(transform.forward, dir) > 0;
            if (inFront)
            {
                Debug.Log($"Can Target {target}");
                _lockOnTarget = target;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        var target = other.GetComponent<ITargetable>();
        if (target != null)
        {
            if (target == _lockOnTarget)
                _lockOnTarget = null;
        }
    }
}

public class CameraTargeting
{
    private Transform _controller;
    private Transform _camera;

    public CameraTargeting(Transform controller)
    {
        _controller = controller;
        _camera = Camera.main.transform;
    }

    public void Update(ITargetable target)
    {
        Transform targetTransform = target.Transform;
        var dir = (targetTransform.position - _controller.position).normalized;
        var quat = Quaternion.Euler(dir);
        _controller.forward = (dir + new Vector3(0, -0.5f, 0)).normalized;
    }

}

public interface ITargetable
{
    Transform Transform { get; }
}