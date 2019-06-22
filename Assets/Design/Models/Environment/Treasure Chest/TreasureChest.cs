using System.Collections;
using System.Linq;
using GEV;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    [SerializeField] private EquippableItemData _itemData;
    [SerializeField] private ScriptableEvent _onChestOpened;
    [SerializeField] private ItemEvent _onItemObtained;
    [SerializeField] private Animator _chestAnimator;

    private void Awake()
    {
    }

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
            _onChestOpened.Raise();
            _chestAnimator.SetTrigger("Open");
        }
    }

}
