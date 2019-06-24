using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Equippable Item")]
public class EquippableItemData : ItemData, IEquippableItem
{
    [SerializeField] GameObject _itemPrefab;
    [SerializeField] EquipmentType _equipmentType;
    [SerializeField] Dialogue _itemObtaineDialogue;
    public GameObject ItemPrefab => _itemPrefab;
    public EquipmentType EquipmentType => _equipmentType;
    public Dialogue ItemObtainedText => _itemObtaineDialogue;
}