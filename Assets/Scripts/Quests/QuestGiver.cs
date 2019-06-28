using System.Collections.Generic;
using UnityEngine;


public class QuestGiver : NPC
{
    [SerializeField] QuestDialogue _questDialogue;
    [SerializeField] GameObject _receiver;
    [SerializeField] QuestData _data;

    [SerializeField] bool _canTalkTo = false;

    Dialogue _current;

    public void ReceiveResponse(bool acceptedQuest)
    {
        if (acceptedQuest)
        {
            if (_receiver.name == name)
            {
                _current = _questDialogue.Waiting;
            }
            else
            {
                _current = _npcDialogue;
                _receiver.AddComponent<QuestReceiver>();
            }

        }
        else
        {
            _current = _questDialogue.Problem; ;
        }
    }

    private void Awake()
    {
        _current = _questDialogue.Problem;
    }

    new void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && _canTalkTo)
        {
            if (GetComponent<QuestReceiver>())
            {

            }
            else
            {
                if (_current == _questDialogue.Problem)
                {
                    FindObjectOfType<DialogueMenu>().StartQuestDialogue(this, _questDialogue);
                }
                else
                {
                    FindObjectOfType<DialogueMenu>().StartDialogue(this, _current);
                }
            }

        }
    }
}

public class QuestReceiver : MonoBehaviour
{
    
}