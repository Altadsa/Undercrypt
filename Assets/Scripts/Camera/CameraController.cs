using System.Collections.Generic;
using System.Linq;
using Cinemachine.Utility;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CameraSettings _settings;
    [SerializeField] Transform _playerTransform;
    [SerializeField] private float _smoothing = 2;

    IAxisInput _cameraAxisInput;
    CameraRotator _cameraRotator;
    CameraCollision _cameraCollision;
    CameraZoom _cameraZoom;
    CameraTargeting _cameraTargeting;

    private List<ITargetable> _potentialTargets = new List<ITargetable>();

    private ITargetable ClosestTarget => _potentialTargets
        .OrderBy(t => Vector3.Distance(t.Transform.position, transform.position))
        .FirstOrDefault();

    private ITargetable _lockOnTarget;

    private Vector3 Origin => _playerTransform.position + Vector3.up;

    private void Awake()
    {
        _cameraAxisInput = _settings.AxisSettings();
        _cameraRotator = _settings.RotationSettings(_cameraAxisInput, transform);
        _cameraCollision = _settings.CollisionSettings(transform);
        _cameraZoom = _settings.ZoomSettings(_playerTransform);
        _cameraTargeting = new CameraTargeting(_playerTransform, transform);
    }

    void Update()
    {
        _cameraAxisInput?.ReadInput();
        _cameraCollision?.CheckForCameraCollision();
        Target();
    }

    private void Target()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log(_lockOnTarget);
            if (_potentialTargets.Count > 0)
                _lockOnTarget = ClosestTarget;
        }
        else
        {
            _lockOnTarget = null;
        }
    }

    private void LateUpdate()
    {
        _cameraRotator.Update();
        if (_lockOnTarget != null)
        {
            _cameraZoom.Update(_lockOnTarget);
            _playerTransform.forward = Vector3.Scale((_lockOnTarget.Transform.position - _playerTransform.position).normalized, new Vector3(1,0,1));
            if (transform.forward.y < 0)
                _cameraTargeting.Update(_lockOnTarget);
            else
                _lockOnTarget = null;
        }
        else
        {
            var distance = Vector3.Distance(transform.position, Origin);
            if (distance > 0.25f)
            {
                transform.position = Vector3.Lerp(transform.position, Origin, Time.deltaTime * _smoothing / distance);
            }
            else
                transform.position = Origin;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponent<ITargetable>();
        if (target != null)
        {
            var dir = (target.Transform.position - transform.position).normalized;
            var inFront = Vector3.Dot(transform.forward, dir) > 0;
            if (inFront)
            {
                Debug.Log($"Can Target {target}");
                if (!_potentialTargets.Contains(target)) _potentialTargets.Add(target);
                Debug.Log(_potentialTargets.Count);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        var target = other.GetComponent<ITargetable>();
        if (target != null)
        {
            if (target == _lockOnTarget)
            {
                _potentialTargets.Remove(_lockOnTarget);
                _lockOnTarget = null;
            }
            else if (!_potentialTargets.Contains(target))
                _potentialTargets.Remove(target);
            Debug.Log(_potentialTargets.Count);
        }
    }
}

public interface ITargetable
{
    Transform Transform { get; }
}