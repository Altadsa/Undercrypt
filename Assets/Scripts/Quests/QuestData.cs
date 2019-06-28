using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Quest Data")]
public class QuestData : ScriptableObject
{
    [SerializeField] int _questId;
    [SerializeField] QuestItem _item;
    public int QuestId => _questId;

    public QuestItem QuestItem => _item;

}