using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DungeonDoor))]
public class BattleDoorCondition : MonoBehaviour, IDoorCondition
{

    public Component DoorCondition => this;

    public bool ConditionsMet()
    {
        throw new System.NotImplementedException();
    }

    public void ConditionMessage()
    {
        throw new System.NotImplementedException();
    }
}

public interface IDoorCondition
{
    bool ConditionsMet();
    void ConditionMessage();
    Component DoorCondition { get; }
}