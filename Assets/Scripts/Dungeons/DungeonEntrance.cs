using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntrance : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player)
        {
            _levelManager.LoadLevel("The Undercrypt");
        }
    }
}
