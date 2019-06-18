using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DummyChest : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook _characterCamera;
    [SerializeField] private CinemachineVirtualCamera _cutsceneCamera;

    [SerializeField] private PlayableAsset _cutscene;
    [SerializeField] private PlayableDirector _director;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player)
        {
            //_cutsceneCamera.Follow = transform;
            _director.Play(_cutscene);
            Debug.Log("Play Open Chest cutscene");

        }
    }

}
