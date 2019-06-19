using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Equippable Item")]
public class EquippableItemData : ItemData, IEquippableItem
{
    [SerializeField] GameObject _itemPrefab;
    public GameObject ItemPrefab => _itemPrefab;
}
