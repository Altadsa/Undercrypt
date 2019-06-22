using GEV;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] ScriptableEvent _onLevelLoaded;

    public void OnLevelLoaded()
    {

    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
    }
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

}
