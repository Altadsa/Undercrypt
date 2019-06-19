using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hookshot : MonoBehaviour
{

    [SerializeField] private GameObject _hook;
    [SerializeField] private float _chainLength;
    [SerializeField] private float speed;

    private Camera _mainCamera;

    private bool _shot = false;

    private Vector3 ext;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
             ext = _mainCamera.transform.forward * _chainLength;
             _hook.transform.position = _mainCamera.transform.position;
             Debug.DrawLine(_mainCamera.transform.position, ext, Color.blue, 10);
            _shot = true;
        }

        if (_shot)
        {
            _hook.transform.position = Vector3.Lerp(_hook.transform.position, ext, Time.deltaTime * speed);
        }
    }
}
