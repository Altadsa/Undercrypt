using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Quest Data")]
public class QuestData : ScriptableObject
{
    [SerializeField] int _questId;
    [SerializeField] QuestItemData _item;
    public int QuestId => _questId;
    public QuestItemData QuestItem => _item;

}