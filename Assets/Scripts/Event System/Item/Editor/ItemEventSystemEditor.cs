using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemEventSystem))]
public class ItemEventSystemEditor : Editor
{
    enum Tab { LISTENERS, EVENT }

    ItemEventSystem system;
    SerializedProperty sListeners;
    List<bool> foldouts = new List<bool>();

    string newEventName;

    int currentTab;

    ItemEvent assingingGameEvent;

    void Awake()
    {
        system = (ItemEventSystem)target;
    }

    public override void OnInspectorGUI()
    {
        var tabTitles = System.Enum.GetNames(typeof(Tab)).ToArray();
        GUILayout.Space(10);
        currentTab = GUILayout.Toolbar(currentTab, tabTitles);
        GUILayout.Space(10);

        switch ((Tab)currentTab)
        {
            case Tab.LISTENERS:
                DrawListenersTab();
                break;

            case Tab.EVENT:
                DrawEventTab();
                break;
        }
    }

    void DrawListenersTab()
    {
        DrawEventAssignmentForm();

        DrawListenerOverview();
    }

    void DrawEventTab()
    {
        DrawEventCreationForm();
    }

    void DrawAssignmentTab()
    {
        DrawDefaultInspector();
    }

    void DrawListenerOverview()
    {
        serializedObject.Update();

        sListeners = serializedObject.FindProperty("listeners");
        UpdateFoldouts(system.listeners);

        if (sListeners.arraySize == 0)
        {
            GUILayout.Label("No Listeners added yet.");
        }
        else
        {
            SerializedProperty sListener, sEvent, sResponse;
            string eventName, foldoutLabel;
            for (int i = 0; i < sListeners.arraySize; i++)
            {
                sListener = sListeners.GetArrayElementAtIndex(i);
                sEvent = sListener.FindPropertyRelative("ItemEvent");
                sResponse = sListener.FindPropertyRelative("Response");
                eventName = sEvent.objectReferenceValue.name;
                foldoutLabel = eventName;

                GUILayout.BeginVertical(GetBoxStyle(20));

                // foldout header
                GUILayout.BeginHorizontal();
                foldouts[i] = EditorGUILayout.Foldout(
                    foldouts[i], foldoutLabel, true);

                if (i == 0)
                {
                    GUILayout.Space(20);
                }
                else
                {
                    if (GUILayout.Button("↑", GUILayout.Width(20)))
                    {
                        sListeners.MoveArrayElement(i, i - 1);
                    }
                }
                if (i == sListeners.arraySize - 1)
                {
                    GUILayout.Space(25);
                }
                else
                {
                    if (GUILayout.Button("↓", GUILayout.Width(20)))
                    {
                        sListeners.MoveArrayElement(i, i + 1);
                    }
                }
                GUILayout.Space(10);
                if (GUILayout.Button("x", GUILayout.Width(20)))
                {
                    sListeners.DeleteArrayElementAtIndex(i);
                    if (i > 0)
                        --i;
                }

                GUILayout.EndHorizontal();
                GUILayout.EndVertical();

                // foldout content
                if (foldouts[i])
                {
                    EditorGUILayout.PropertyField(sEvent);
                    EditorGUILayout.PropertyField(sResponse);

                    GUILayout.Space(20);
                }
            }
        }

    }

    void DrawEventCreationForm()
    {
        GUILayout.BeginVertical(GetBoxStyle());
        GUILayout.Label("Create new Item Event");

        newEventName = GUILayout.TextField(newEventName);
        if (GUILayout.Button("Create"))
        {
            CreateEvent(newEventName);
            newEventName = string.Empty;
        }

        GUILayout.EndVertical();
        GUILayout.Space(15);
    }

    void DrawEventAssignmentForm()
    {
        GUILayout.BeginVertical(GetBoxStyle());
        GUILayout.Label("Add new ItemEvent Listener");
        assingingGameEvent = (ItemEvent)EditorGUILayout.ObjectField(
            assingingGameEvent, typeof(ItemEvent), false);
        if (GUILayout.Button("Add"))
        {
            if (assingingGameEvent != null)
            {
                CreateListener(assingingGameEvent);
                assingingGameEvent = null;
            }
        }
        GUILayout.EndVertical();
    }

    void CreateEvent(string eventName)
    {
        if (!string.IsNullOrEmpty(eventName))
        {
            var asset = CreateInstance<ItemEvent>();
            var assetPath = string.Format(
                "Assets/Scriptable Objects/Events/Item Events/{0}.asset",
                eventName);
            AssetDatabase.CreateAsset(asset, assetPath);
            AssetDatabase.SaveAssets();
        }
    }

    void CreateListener(ItemEvent itemEvent)
    {
        var listener = new ItemEventListener
        {
            ItemEvent = itemEvent
        };
        system.listeners.Add(listener);
    }

    void ExpandFoldouts()
    {
        foldouts.ForEach(f => f = true);
    }

    void CollapseFoldouts()
    {
        foldouts.ForEach(f => f = false);
    }

    void UpdateFoldouts(List<ItemEventListener> listeners)
    {
        while (foldouts.Count < listeners.Count)
        {
            foldouts.Add(false);
        }

        while (foldouts.Count > listeners.Count)
        {
            foldouts.RemoveAt(foldouts.Count - 1);
        }
    }

    static GUIStyle GetBoxStyle(int leftOffset = 10)
    {
        GUIStyle style = new GUIStyle(GUI.skin.box)
        {
            padding = new RectOffset(leftOffset, 10, 10, 10)
        };
        return style;
    }
}