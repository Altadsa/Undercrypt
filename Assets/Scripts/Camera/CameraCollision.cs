﻿using System;
using UnityEngine;

public class CameraCollision 
{
    const float CAMERA_DISTANCE = 7.5f;

    readonly Transform _camera;
    readonly Transform _controller;
    readonly Int32 _playerLayerMask = ~(1 << LayerMask.NameToLayer("Characters"));
    Vector3 _cameraMax => _controller.position + _camera.forward * -CAMERA_DISTANCE;



    public CameraCollision(Transform camera, Transform controller)
    {
        _camera = camera;
        _controller = controller;
    }

    public void CheckForCameraCollision()
    {
        RaycastHit hit;
        bool hasHit = Physics.Linecast(_controller.position, _cameraMax, out hit, _playerLayerMask);
        _camera.position = !hasHit ? _camera.position : hit.point;
    }
}