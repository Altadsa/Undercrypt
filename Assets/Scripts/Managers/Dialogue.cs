using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Dialogue")]
public class Dialogue : ScriptableObject
{
    [TextArea(1, 3)] [SerializeField] private string[] _dialogueSentences;

    public string[] Sentences => _dialogueSentences;
}
