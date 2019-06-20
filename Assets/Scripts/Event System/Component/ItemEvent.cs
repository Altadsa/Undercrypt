using System;
using System.Collections;
using System.Collections.Generic;
using GEV;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "GEV/Component Event")]
public class ItemEvent : ScriptableObject
{
    List<ItemEventListener> listeners = new List<ItemEventListener>();

    public void Raise(EquippableItemData itemData)
    {
        foreach (var itemEventListener in listeners)
        {
            itemEventListener.OnEventRaised(itemData);
        }
    }

    public void RegisterListener(ItemEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(ItemEventListener listener)
    {
        listeners.Remove(listener);
    }
}

[System.Serializable]
public class ItemEventListener : IEventListener
{
    public ItemEvent Event;
    public UnityEventItem Response;

    public void Register()
    {
        Event.RegisterListener(this);
    }

    public void Unregister()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(EquippableItemData itemData)
    {
        Response.Invoke(itemData);
    }
}

public class ItemEventSystem : GameEventSystem<ItemEventListener> { }


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

[System.Serializable]
public class UnityEventItem : UnityEvent<EquippableItemData> { }

public interface IEventListener
{
    void Register();
    void Unregister();
}

[CustomEditor(typeof(ItemEvent))]
public class ItemEventEditor : Editor
{

}

[CustomEditor(typeof(ItemEventSystem))]
public class ItemEventSystemEditor : Editor
{

}
