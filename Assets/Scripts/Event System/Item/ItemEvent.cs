using System;
using System.Collections;
using System.Collections.Generic;
using GEV;
using UnityEngine;

[CreateAssetMenu(menuName = "GEV/Item Event")]
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


public interface IEventListener
{
    void Register();
    void Unregister();
}