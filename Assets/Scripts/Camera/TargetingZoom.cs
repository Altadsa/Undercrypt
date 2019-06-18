using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TargetingZoom : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook _targetingCamera;
    [SerializeField] CinemachineCameraOffset _offset;
    [SerializeField] CinemachineTargetGroup _targets;

    float _minScale = 1, _maxScale = 2;
    float _scale = 1;
    float lastDistance = 0;

    CinemachineFreeLook.Orbit[] _camOrbits;

    private void Awake()
    {
        _camOrbits = new CinemachineFreeLook.Orbit[_targetingCamera.m_Orbits.Length];
        for (int i = 0; i < _camOrbits.Length; i++)
        {
            _camOrbits[i] = _targetingCamera.m_Orbits[i];
        }
    }

    private void LateUpdate()
    {
        if (_targets.m_Targets.Length > 1)
        {
            var p = _targets.m_Targets[0].target.position;
            var t = _targets.m_Targets[1].target.position;
            var d = Vector3.Distance(p, t);

            
            var diff = d - lastDistance;
            if (d <= 5)
                _scale = _minScale;
            else
            {
                _scale = Mathf.Clamp(_scale + diff, _minScale, _maxScale);
            }


            for (int i = 0; i < _camOrbits.Length; i++)
            {
                _targetingCamera.m_Orbits[i].m_Height = _camOrbits[i].m_Height * _scale;
                _targetingCamera.m_Orbits[i].m_Radius = _camOrbits[i].m_Radius * _scale;
            }

            lastDistance = d;

        }
    }

}
