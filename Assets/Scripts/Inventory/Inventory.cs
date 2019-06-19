using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public Transform EquipmentParent;

    public List<IEquippableItem> EquippableItems { get; private set; } = new List<IEquippableItem>();
    public List<IConsumableItem> ConsumableItems { get; private set; } = new List<IConsumableItem>();

    public IEquippableItem EquippedItem = null;

    public event Action UpdateInventory;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    public void EquipItem(int id)
    {
        EquippedItem?.ItemPrefab.SetActive(false);
        EquippedItem = EquippableItems.Find(i => i.ItemId == id);
        EquippedItem.ItemPrefab.SetActive(true);
        UpdateInventory?.Invoke();
    }

    public void AddEquippable(EquippableItemData data)
    {
        bool inInventory = EquippableItems.Exists(i => i.ItemId == data.ItemId);
        if (!inInventory)
        {
            EquippableItems.Add(new EquippableItem(data, EquipmentParent));
            UpdateInventory?.Invoke();
        }
        else
            Debug.Log($"Adventurer already has {data.Name}");
    }

}


public interface IItem
{
    int ItemId { get; }
    Sprite Icon { get; }
    string Name { get; }
    string Description { get; }
}

public interface IEquippableItem : IItem
{
    GameObject ItemPrefab { get; }
}

public interface IConsumableItem : IItem
{
    int Quantity { get; }
}