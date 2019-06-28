using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueMenu : MonoBehaviour
{
    [SerializeField] TMP_Text _npcName;
    [SerializeField] TMP_Text _dialogue;
    [SerializeField] TMP_Text _proposal;
    [SerializeField] TMP_Text _accept;
    [SerializeField] TMP_Text _decline;
    [SerializeField] GameObject _dialogueGo;
    [SerializeField] GameObject _questGo;

    Dialogue _current;
    QuestDialogue _questDialogue;
    NPC _target;

    int _sentenceIndex = 0;

    public void StartQuestDialogue(QuestGiver questGiver, QuestDialogue questDialogue)
    {
        _target = questGiver;
        _questDialogue = questDialogue;
        _current = questDialogue.Problem;
        _npcName.text = _target.name;
        GetNextSentence();
    }

    public void StartDialogue(NPC questGiver, Dialogue dialogue)
    {
        _target = questGiver;
        _current = dialogue;
        _questDialogue = null;
    }

    public void ReceiveResponse(bool accept)
    {
        _sentenceIndex = 0;
        var qTarget = _target as QuestGiver;
        qTarget.ReceiveResponse(accept);
        ToggleDialogueBoxes(true);
        StartCoroutine(accept
            ? ShowText(_questDialogue.AcceptResponse, _dialogue)
            : ShowText(_questDialogue.DeclineResponse, _dialogue));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetNextSentence();
        }
    }

    public void GetNextSentence()
    {
        StopAllCoroutines();
        if (_sentenceIndex < _current.Sentences.Length)
        {
            ToggleDialogueBoxes(true);
            StartCoroutine(ShowText(_current.Sentences[_sentenceIndex], _dialogue));
            _sentenceIndex++;
        }
        else
        {
            if (_questDialogue)
            {
                ToggleDialogueBoxes(false);
                ShowQuestProposal();
            }
        }
    }

    private void ToggleDialogueBoxes(bool toggle)
    {
        _dialogueGo.SetActive(toggle);
        _questGo.SetActive(!toggle);
    }

    IEnumerator ShowDialogue()
    {
        var sentence = _current.Sentences[_sentenceIndex];
        _sentenceIndex++;
        foreach (var chr in sentence.ToCharArray())
        {
            _dialogue.text += chr;
            yield return new WaitForSeconds(1 / sentence.Length);
        }
    }

    private void ShowQuestProposal()
    {
        StartCoroutine(ShowText(_questDialogue.Proposal, _proposal));
        StartCoroutine(ShowText(_questDialogue.Accept, _accept));
        StartCoroutine(ShowText(_questDialogue.Decline, _decline));
    }

    IEnumerator ShowText(string sentence, TMP_Text textToAmend)
    {
        textToAmend.text = "";
        foreach (var chr in sentence.ToCharArray())
        {
            textToAmend.text += chr;
            yield return new WaitForSeconds(1 / sentence.Length);
        }
    }
}