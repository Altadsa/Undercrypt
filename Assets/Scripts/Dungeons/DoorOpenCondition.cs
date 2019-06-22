using UnityEngine;

[RequireComponent(typeof(DungeonDoor))]
public abstract class DoorOpenCondition : MonoBehaviour
{
    public abstract bool ConditionsMet();
    public abstract void ConditionMessage();
}