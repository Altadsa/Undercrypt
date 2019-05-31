using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Float Variable")]
public class FloatVariable : ScriptableObject
{
    [Range(0,1)]
    public float Value;

    public event Action OnValueChanged;

    public void OnValueChange()
    {
        OnValueChanged?.Invoke();
    }

}