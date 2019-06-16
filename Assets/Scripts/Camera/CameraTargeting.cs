using UnityEngine;

public class CameraTargeting
{
    private Transform _player;
    private Transform _controller;
    private float _transition;

    private Camera _mainCamera;

    private bool _inPosition = false;

    public CameraTargeting(Transform player,Transform controller)
    {
        _player = player;
        _controller = controller;
        _mainCamera = Camera.main;
    }

    public void Update(ITargetable target)
    {
        if (target == null)
        {
            _inPosition = false;
            return;
        }




        Transform targetTransform = target.Transform;
        var dir = (targetTransform.position - _player.position).normalized;
        var dist = Vector3.Distance(_player.position, targetTransform.position) / 2;
        var newPosition = _player.position + dir * dist;
        var distance = Vector3.Distance(_controller.position, newPosition);
        if (distance > 0)
        {
            _controller.position = Vector3.Lerp(_controller.position, newPosition, Time.deltaTime * distance);
        }

    }

}