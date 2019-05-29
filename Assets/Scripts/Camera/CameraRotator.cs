using UnityEngine;

[System.Serializable]
public class CameraRotator
{
    private const float MIN_VERTICAL_ROTATION = 10, MAX_VERTICAL_ROTATION = 90;

    private float _xSensitivity = 60f;
    private float _ySensitivity = 15f;
    private IAxisInput _camAxisInput;
    private Transform _cameraArm;

    private bool RotationButtonsPressed => Input.GetMouseButton(1) || Input.GetMouseButton(0);

    public CameraRotator(IAxisInput cameraAxisInput, Transform cameraArm)
    {
        _camAxisInput = cameraAxisInput;
        _cameraArm = cameraArm;
    }

    public void RotateCamera()
    {
        SetCursorMode();
        _cameraArm.eulerAngles = Rotate();
    }

    private void SetCursorMode()
    {
        if (RotationButtonsPressed)
            LockCursor();
        else
            UnlockCursor();
    }

    Vector3 Rotate()
    {
        return RotationButtonsPressed ? CalculateCameraRotation() : _cameraArm.eulerAngles;
    }
    protected Vector3 CalculateCameraRotation()
    {
        LockCursor();
        float rX = Mathf.Clamp(_cameraArm.eulerAngles.x - _camAxisInput.Vertical * _ySensitivity * Time.deltaTime, MIN_VERTICAL_ROTATION, MAX_VERTICAL_ROTATION);
        float rY = _cameraArm.eulerAngles.y + _camAxisInput.Horizontal * _xSensitivity * Time.deltaTime;
        return new Vector3(rX, rY, 0);
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

