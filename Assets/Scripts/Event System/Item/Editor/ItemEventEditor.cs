using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemEvent))]
public class ItemEventEditor : Editor
{
    private EquippableItemData _data;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ItemEvent itemEvent = (ItemEvent) target;
        _data = EditorGUILayout.ObjectField(_data, typeof(EquippableItemData), true) as EquippableItemData;
        if (GUILayout.Button("Raise ItemEvent"))
        {
            itemEvent.Raise(_data);
            _data = null;
        }
    }
}