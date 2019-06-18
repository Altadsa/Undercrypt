using UnityEngine;

public class CameraTargeting
{
    private Transform _player;
    private Transform _controller;
    private float _transition;

    private Camera _mainCamera;

    private bool _inPosition = false;

    readonly Rect Targetzone = new Rect((Screen.width / 10), (Screen.height / 10), Screen.width * 0.8f,
        Screen.height * 0.8f);

    public CameraTargeting(Transform player,Transform controller)
    {
        _player = player;
        _controller = controller;
        _mainCamera = Camera.main;
        Debug.Log($"Screen Width {Screen.width} Screen Height {Screen.height}");
        Debug.Log(Targetzone);
    }

    public void Update(ITargetable target)
    {
        if (target == null)
        {
            _inPosition = false;
            return;
        }

        var playerSp = _mainCamera.WorldToScreenPoint(_player.position);
        var targetSp = _mainCamera.WorldToScreenPoint(target.Transform.position);

        Debug.Log($"Player Screen Pos: {playerSp} Target Screen Pos{targetSp}");

        AdjustCameraPosition(target);


    }

    void AdjustCameraPosition(ITargetable target)
    {
        var midpoint = CalculateMidpoint(target.Transform);
        var distance = Vector3.Distance(_controller.position, midpoint);
        if (distance > 0)
        {
            _mainCamera.transform.forward = Vector3.Lerp(_mainCamera.transform.forward,
                (midpoint - _mainCamera.transform.position).normalized, Time.deltaTime);
            _controller.position = Vector3.Lerp(_controller.position, midpoint, Time.deltaTime * distance);
        }
    }

    private Vector3 CalculateMidpoint(Transform targetTransform)
    {
        var dir = (targetTransform.position - _player.position).normalized;
        var dist = Vector3.Distance(_player.position, targetTransform.position) / 2;
        return _player.position + dir * dist + Vector3.up;
    }

}