using UnityEngine;

public abstract class OpenCondition : MonoBehaviour
{
    public abstract bool ConditionsMet();
    public abstract void ConditionMessage();
}