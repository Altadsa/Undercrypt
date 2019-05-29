using UnityEngine;

public class MouseCameraAxisInput : IAxisInput
{
    public void ReadInput()
    {
        Horizontal = Input.GetAxis("Mouse X");
        Vertical = Input.GetAxis("Mouse Y");
    }

    public bool HasAxisInput => (Mathf.Abs(Horizontal) > Mathf.Epsilon) || (Mathf.Abs(Vertical) > Mathf.Epsilon);

    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
}