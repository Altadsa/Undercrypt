using System.Collections;
using System.Collections.Generic;
using GEV;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private ScriptableEvent _onDialogueFinished;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _itemImage;

    private string[] _dialogueSentences;
    private int _currentSentenceIndex = 0;


    public void OnItemObtained(EquippableItemData itemData)
    {
        _dialogueSentences = itemData.ItemObtainedText.Sentences;
        _itemImage.sprite = itemData.Icon;
        StartCoroutine(TypeSentence());
    }

    public void GetNextSentence()
    {
        StopAllCoroutines();
        if (_currentSentenceIndex < _dialogueSentences.Length)
        {
            _text.text = "";
            StartCoroutine(TypeSentence());
        }
        else
        {
            _currentSentenceIndex = 0;
            _onDialogueFinished.Raise();
        }
    }

    IEnumerator TypeSentence()
    {
        string sentence = _dialogueSentences[_currentSentenceIndex];
        _currentSentenceIndex++;
        foreach (var character in sentence.ToCharArray())
        {
            _text.text += character;
            yield return new WaitForSeconds(1 / sentence.Length);
        }
        
    }
}
