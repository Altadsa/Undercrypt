using UnityEngine;

[RequireComponent(typeof(DungeonDoor))]
public class KeyDoorCondition : MonoBehaviour, IDoorCondition
{

    public Component DoorCondition => this;

    public bool ConditionsMet()
    {
        var inventory = Inventory.Instance;
        if (inventory.Keys > 0)
        {
            inventory.ChangeKeyCount(-1);
            return true;
        }

        return false;
    }

    public void ConditionMessage()
    {
        Debug.Log("You need a Key to open this door.");
    }
}