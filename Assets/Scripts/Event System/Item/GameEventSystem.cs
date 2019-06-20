using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventSystem<T> : MonoBehaviour
    where T : IEventListener
{
    public List<T> listeners = new List<T>();

    private void OnEnable()
    {
        foreach (var eventListener in listeners)
        {
            eventListener.Register();
        }
    }

    private void OnDisable()
    {
        foreach (var eventListener in listeners)
        {
            eventListener.Unregister();
        }
    }
}