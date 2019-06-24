using System.Collections.Generic;
using System.Linq;
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (_lockOnTarget == null)
            {
                if (_potentialTargets.Count > 0)
                    _lockOnTarget = ClosestTarget;
            }
            else
            {
                _lockOnTarget = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _faceForward = true;
        }

    }

    private bool _faceForward = false;
    private void LateUpdate()
    {
        _cameraRotator.Update();
        _cameraZoom.Update(_lockOnTarget);
        if (_lockOnTarget != null)
        {
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

            if (false)
            {
                var angle = Vector3.Angle(transform.forward, _playerTransform.forward);
                transform.forward = Vector3.Lerp(transform.forward, _playerTransform.forward, angle * Time.deltaTime);
                _faceForward = false;
            }
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

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}

