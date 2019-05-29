using UnityEngine;

public class CameraZoom
{
    private readonly Transform _camera;
    private readonly float _zoomSpeed;
    private readonly Vector3 _minZoomDistance = new Vector3(0,0,0);
    private readonly Vector3 _maxZoomDistance = new Vector3(0,0,-10f);

    private float ZoomDelta => Input.mouseScrollDelta.y;

    public CameraZoom(Transform camera, float zoomSpeed)
    {
        _camera = camera;
        _zoomSpeed = zoomSpeed;
    }

    public void Update()
    {
        _camera.localPosition = Zoom();
    }

    private Vector3 Zoom()
    {
        float zoom = ZoomDelta * _zoomSpeed * Time.deltaTime * Mathf.Abs(_camera.forward.z);
        Vector3 newZoom = _camera.localPosition;
        newZoom.z = Mathf.Clamp(newZoom.z + zoom, _maxZoomDistance.z, _minZoomDistance.z);
        return newZoom;
    }

}