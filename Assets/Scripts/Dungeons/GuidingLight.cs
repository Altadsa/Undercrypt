using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidingLight : MonoBehaviour
{
    [SerializeField] private Transform[] _guides;

    private const float BASE_DURATION = 0.5f;

    private Transform _start, _end;
    private int _currentGuide;
    private float _duration;

    private bool _reverse = false;

    private float _startTime = 0;

    private float NewDuration => BASE_DURATION * Vector3.Distance(transform.position, _guides[_currentGuide].position);

    private void OnEnable()
    {
        transform.position = _guides[0].position;
        _startTime = Time.time;
        _currentGuide++;
        _duration = NewDuration;
    }

    private void OnDisable()
    {

    }

    private void Update()
    {
        var nextPos = _guides[_currentGuide].position;
        var distToNextGuide = Vector3.Distance(transform.position, nextPos);
        float delta = Time.time - _startTime;
        delta /= _duration;
        if (delta > 1)
        {
            delta = 1;
            _startTime = Time.time;
            if (!_reverse)
            {
                if (_currentGuide < _guides.Length - 1)
                {
                    _currentGuide++;
                }
                else
                {
                    _reverse = true;
                    _currentGuide--;
                }
            }
            else
            {
                if (_currentGuide > 0)
                {
                    _currentGuide--;
                }
                else
                {
                    _reverse = false;
                }
            }

            _duration = NewDuration;
        }

        transform.position = Vector3.Lerp(transform.position, nextPos, delta);
    }
}
