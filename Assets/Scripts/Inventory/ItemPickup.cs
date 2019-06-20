using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] EquippableItemData _data;
    [SerializeField] ItemEvent _onItemObtained;

    private void Obtain()
    {
        _onItemObtained.Raise(_data);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player)
            Obtain();
    }
}
