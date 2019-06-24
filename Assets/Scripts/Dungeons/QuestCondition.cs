using UnityEngine;

public class QuestCondition : OpenCondition
{
    [SerializeField] QuestItemData _data;

    public override bool ConditionsMet()
    {
        var inventory = Inventory.Instance;
        if (inventory.QuestItems.Exists(i => i.ItemId == _data.ItemId))
        {
            inventory.RemoveQuestItem(_data.ItemId);
            return true;
        }

        return false;
    }

    public override void ConditionMessage()
    {
        Debug.Log($"You need the {_data.Name}");
    }
}