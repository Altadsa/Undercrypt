using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    public bool Enabled = false;

    [SerializeField] GameObject _roomAssets;


    private void Awake()
    {
        //_roomAssets.SetActive(Enabled);
    }

    public void ToggleRoom()
    {
        Enabled = !Enabled;
        _roomAssets.SetActive(Enabled);
    }
}