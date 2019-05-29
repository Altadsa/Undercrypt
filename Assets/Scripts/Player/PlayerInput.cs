using UnityEngine;

public class PlayerInput : IAxisInput
{
    public void ReadInput()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
    }

    public bool HasAxisInput => Mathf.Abs(Horizontal) >= Mathf.Epsilon || Mathf.Abs(Vertical) >= Mathf.Epsilon;
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
}