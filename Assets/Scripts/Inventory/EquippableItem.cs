using UnityEngine;

public class EquippableItem : IEquippableItem
{
    public Transform EquipTransform;

    public int ItemId { get; set;}
    public Sprite Icon { get; set; }
    public string Name { get; set;}
    public string Description { get; set; }
    public GameObject ItemPrefab { get; set;}

    public EquippableItem(EquippableItemData data, Transform equipTransform)
    {
        EquipTransform = equipTransform;
        ItemId = data.ItemId;
        Icon = data.Icon;
        Name = data.Name;
        Description = data.Description;
        ItemPrefab = Object.Instantiate(data.ItemPrefab, EquipTransform);
        ItemPrefab.SetActive(false);
    }
}