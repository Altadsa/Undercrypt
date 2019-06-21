using UnityEngine;

public class CameraFade : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] string _levelToFadeTo;


    public void FadeToLevel()
    {
        _levelManager.LoadLevel(_levelToFadeTo);
    }

}
