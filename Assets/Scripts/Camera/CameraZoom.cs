using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraZoom
{
    private readonly Transform _player;
    private readonly Transform _camera;
    private readonly float _zoomSpeed;
    private readonly Vector3 _minZoomDistance = new Vector3(0,0,0);
    private readonly Vector3 _maxZoomDistance = new Vector3(0,0,-10f);

    private readonly Vector3 _origin;

    private float ZoomDelta => Input.mouseScrollDelta.y;

    private readonly Camera _mainCamera;

    public CameraZoom(Transform player, float zoomSpeed)
    {
        _player = player;
        _mainCamera = Camera.main;
        _camera = _mainCamera.transform;
        _zoomSpeed = zoomSpeed;
        _origin = _camera.localPosition;

    }

    public void Update(ITargetable target)
    {
        _camera.localPosition = Zoom(target);
    }

    private Vector3 Zoom(ITargetable targetable)
    {
        var playerVp = _mainCamera.WorldToViewportPoint(_player.position);
        var targetVp = _mainCamera.WorldToViewportPoint(targetable.Transform.position);
        var distance = Vector3.Distance(playerVp, targetVp);

        var viewPortDist = playerVp - targetVp;
        var newPos = Vector3.zero;
        if (Mathf.Abs(viewPortDist.x) > 0.5f || Mathf.Abs(viewPortDist.y) > 0.5f)
        {
            newPos = _origin + Vector3.forward * _camera.forward.z * distance / 2;
        }
        else
        {
            newPos = _origin;
        }

        return Vector3.Lerp(_camera.localPosition, newPos, Time.deltaTime * distance / 2);
        //distance = Vector3.Distance(_camera.localPosition, _origin);
        //return Vector3.Lerp(_camera.localPosition, _origin, Time.deltaTime * distance);
    }

}