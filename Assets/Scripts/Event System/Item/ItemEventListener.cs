[System.Serializable]
public class ItemEventListener : IEventListener
{
    public ItemEvent ItemEvent;
    public UnityEventItem Response;

    public void Register()
    {
        ItemEvent.RegisterListener(this);
    }

    public void Unregister()
    {
        ItemEvent.UnregisterListener(this);
    }

    public void OnEventRaised(EquippableItemData itemData)
    {
        Response.Invoke(itemData);
    }
}