using GEV;
using UnityEngine;

public class DungeonEntrance : MonoBehaviour
{
    [SerializeField] private ScriptableEvent _onDungeonEntered;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player)
        {
            Debug.Log("Entering Dungeon");
            _onDungeonEntered.Raise();
        }
    }
}
