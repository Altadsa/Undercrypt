using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardGate : MonoBehaviour
{
    [SerializeField] QuestCondition questCondition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (questCondition.ConditionsMet())
            {
                gameObject.SetActive(false);
            }
            else
            {
                questCondition.ConditionMessage();
            }
        }
    }
}
