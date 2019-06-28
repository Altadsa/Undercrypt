using UnityEngine;
using UnityEngine.EventSystems;

public class QuestResponse : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] DialogueMenu _dialogueMenu;
    [SerializeField] bool _Accept;
    [SerializeField] KeyCode trigger;

    private void Update()
    {
        if (Input.GetKeyDown(trigger))
        {
            _dialogueMenu.ReceiveResponse(_Accept);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _dialogueMenu.ReceiveResponse(_Accept);
    }
}