using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class Z_Targeting : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup _targets;
    [SerializeField] private Transform _target;

    private List<ITargetable> _potentialTargets = new List<ITargetable>();

    private ITargetable ClosestTarget => _potentialTargets
        .OrderBy(t => Vector3.Distance(t.Transform.position, transform.position))
        .FirstOrDefault();

    public static  ITargetable PlayerTarget { get; private set; }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (PlayerTarget == null && _potentialTargets.Count > 0)
            {
                PlayerTarget = ClosestTarget;
                //_targets.m_Targets[1].target = PlayerTarget.Transform;
            }

        }
        else
        {
            if (PlayerTarget != null)
            {
                PlayerTarget = null;
                //_targets.m_Targets[1].target = null;
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
            if (target == PlayerTarget)
            {
                _potentialTargets.Remove(PlayerTarget);
                PlayerTarget = null;
            }
            else if (!_potentialTargets.Contains(target))
                _potentialTargets.Remove(target);
            Debug.Log(_potentialTargets.Count);
        }
    }
}
