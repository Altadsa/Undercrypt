using UnityEngine;

public class JoystickCameraAxisInput : IAxisInput
{
    public void ReadInput()
    {
        Horizontal = Input.GetAxis("Right Stick X");
        Vertical = Input.GetAxis("Right Stick Y");
    }

    public bool HasAxisInput => (Mathf.Abs(Horizontal) > Mathf.Epsilon) || (Mathf.Abs(Vertical) > Mathf.Epsilon);

    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
}