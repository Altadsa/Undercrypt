using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GEV/Dialogue Event")]
public class DialogueEvent : ScriptableObject
{
    List<DialogueEventListener> listeners = new List<DialogueEventListener>();

    public void Raise(Dialogue dialogue)
    {
        foreach (var dialogueEventListener in listeners)
        {
            dialogueEventListener.OnEventRaised(dialogue);
        }
    }

    public void RegisterListener(DialogueEventListener listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(DialogueEventListener listener)
    {
        listeners.Remove(listener);
    }
}