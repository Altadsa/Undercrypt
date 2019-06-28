[System.Serializable]
public class DialogueEventListener : IEventListener
{
    public DialogueEvent DialogueEvent;
    public UnityEventDialogue Response;

    public void Register()
    {

    }

    public void Unregister()
    {

    }

    public void OnEventRaised(Dialogue dialogue)
    {
        Response.Invoke(dialogue);
    }
}