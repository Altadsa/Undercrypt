using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Sound Float Variable")]
public class SoundFloatVariable : FloatVariable
{
    [SerializeField] private FloatVariable _masterVolume;

    public float MasterValue => Value * _masterVolume.Value;

    private void OnEnable()
    {
        if (_masterVolume)
        {
            _masterVolume.OnValueChanged += OnValueChange;
        }
    }

}