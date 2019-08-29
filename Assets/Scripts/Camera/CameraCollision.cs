using System;
using UnityEngine;

public class CameraCollision 
{
    const float CAMERA_DISTANCE = 7.5f;

    readonly Transform _camera;
    readonly Transform _controller;
    readonly Int32 _playerLayerMask = ~(1 << LayerMask.NameToLayer("Characters"));

    private Vector3 _defaultPosition => _controller.position - (_camera.forward * 4);
    
    Vector3 _cameraMax => _controller.position + _camera.forward * -CAMERA_DISTANCE;



    public CameraCollision(Transform controller)
    {
        _camera = Camera.main.transform;
        _controller = controller;
    }

    private float _duration = 1;
    public void CheckForCameraCollision()
    {
        RaycastHit hit;
        bool hasHit = Physics.Linecast(_controller.position, _cameraMax, out hit, (_playerLayerMask));
        var moveDestination = !hasHit ? _defaultPosition : hit.point;
        //_camera.position = !hasHit ? _defaultPosition : hit.point;
        _camera.position = Vector3.Lerp(_camera.position, moveDestination, _duration / Time.deltaTime);
    }
}