using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Equippable Item")]
public class EquippableItemData : ItemData, IEquippableItem
{
    [SerializeField] GameObject _itemPrefab;
    [SerializeField] private EquipmentType _equipmentType;
    public GameObject ItemPrefab => _itemPrefab;
    public EquipmentType EquipmentType => _equipmentType;
}
