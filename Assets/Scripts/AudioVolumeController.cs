using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeController : MonoBehaviour
{
    [SerializeField] private FloatVariable _volume;
    [SerializeField] private TMP_Text _audioDescription;
    private Slider _volumeSlider;


    private void Awake()
    {
        _volumeSlider = GetComponent<Slider>();
        _volumeSlider.value = _volume.Value;
        _audioDescription.text = $"{_volume.name} : {Math.Round(_volume.Value, 2)}";
    }

    public void OnVolumeChanged()
    {
        _volume.Value = _volumeSlider.value;
        _audioDescription.text = $"{_volume.name} : {Math.Round(_volume.Value, 2)}";
        _volume.OnValueChange();
    }
    
}