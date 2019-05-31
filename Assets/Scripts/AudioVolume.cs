using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioVolume : MonoBehaviour
{
    [SerializeField] private SoundFloatVariable _volume;
    private AudioSource _audioSrc;

    private void Awake()
    {
        _audioSrc = GetComponent<AudioSource>();
        _audioSrc.volume = _volume.MasterValue;
        _volume.OnValueChanged += OnVolumeChanged;
    }

    public void OnVolumeChanged()
    {
        _audioSrc.volume = _volume.MasterValue;
    }

}