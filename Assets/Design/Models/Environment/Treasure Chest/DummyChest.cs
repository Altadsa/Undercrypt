using UnityEngine;

public class DummyChest : MonoBehaviour
{
    [SerializeField] private EquippableItemData _itemData;
    [SerializeField] private ItemEvent _onItemObtained;
    [SerializeField] private Animator _chestAnimator;

    public void OnItemObtained()
    {
        _onItemObtained.Raise(_itemData);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        var player = other.GetComponent<PlayerController>();
        if (player)
        {
            _chestAnimator.SetTrigger("Open");
        }
    }

}
