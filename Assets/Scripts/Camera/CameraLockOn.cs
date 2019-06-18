using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class CameraLockOn : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup _targetCentre;

    private List<ITargetable> _potentialTargets = new List<ITargetable>();

    private ITargetable ClosestTarget => _potentialTargets
        .OrderBy(t => Vector3.Distance(t.Transform.position, transform.position))
        .FirstOrDefault();

    private ITargetable _lockOnTarget;

    private void Awake()
    {
        _targetCentre.m_Targets[1].target = null;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _lockOnTarget = ClosestTarget;
            if (_lockOnTarget != null)
                _targetCentre.m_Targets[1].target = _lockOnTarget.Transform;
            else
                _targetCentre.m_Targets[1].target = null;
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
                Debug.Log($"Can no longer Target {target}");
                _potentialTargets.Remove(_lockOnTarget);
                _lockOnTarget = null;
            }
            else if (!_potentialTargets.Contains(target))
                _potentialTargets.Remove(target);
            Debug.Log(_potentialTargets.Count);
        }
    }
}