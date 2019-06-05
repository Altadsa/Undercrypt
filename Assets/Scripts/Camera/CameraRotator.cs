using UnityEngine;

public class CameraRotator
{
    private const float MIN_VERTICAL_ROTATION = 10, MAX_VERTICAL_ROTATION = 90;

    private float _xSensitivity = 60f;
    private float _ySensitivity = 15f;
    private IAxisInput _camAxisInput;
    private Transform _cameraArm;


    private Transform _lockedTarget;
    private bool _targetLocked = false;

    private bool RotationButtonsPressed => Input.GetMouseButton(1) || Input.GetMouseButton(0);

    public CameraRotator(IAxisInput cameraAxisInput, Transform cameraArm)
    {
        _camAxisInput = cameraAxisInput;
        _cameraArm = cameraArm;
    }

    public void RotateCamera()
    {
        if (!_targetLocked)
        {
            LockCursor();
            _cameraArm.eulerAngles = CalculateCameraRotation(); 
        }
        else
        {
            if (!_lockedTarget)
                LockTarget(null);
            Vector3 targetDirection = _lockedTarget.position - _cameraArm.position;
            targetDirection.Normalize();
            //targetDirection.y = 0;

            if (targetDirection == Vector3.zero)
                targetDirection = _cameraArm.forward;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            _cameraArm.rotation = Quaternion.Slerp(_cameraArm.rotation, targetRotation, 9);

        }
    }

    public void LockTarget(Transform _target)
    {
        _lockedTarget = _target;
        _targetLocked = _target;
    }

    private void SetCursorMode()
    {
        if (RotationButtonsPressed)
            LockCursor();
        else
            UnlockCursor();
    }


    protected Vector3 CalculateCameraRotation()
    {
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

