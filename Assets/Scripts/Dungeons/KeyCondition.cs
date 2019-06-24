using UnityEngine;

[RequireComponent(typeof(DungeonDoor))]
public class KeyCondition : OpenCondition
{
    
    public override bool ConditionsMet()
    {
        var inventory = Inventory.Instance;
        if (inventory.Keys > 0)
        {
            inventory.ChangeKeyCount(-1);
            return true;
        }

        return false;
    }

    public override void ConditionMessage()
    {
        Debug.Log("You need a Key to open this door.");
    }
}