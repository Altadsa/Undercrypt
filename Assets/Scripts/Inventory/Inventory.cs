using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public Transform EquipmentParent;

    public int Arrows { get; private set; } = 10;
    public int Keys { get; private set; } = 0;

    public List<IEquippableItem> EquippableItems { get; private set; } = new List<IEquippableItem>();
    public List<IConsumableItem> ConsumableItems { get; private set; } = new List<IConsumableItem>();

    public IEquippableItem EquippedItem = null;

    public event Action UpdateEquipment;
    public event Action UpdateConsumable; 


    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    public void EquipItem(int id)
    {
        EquippedItem?.ItemPrefab.SetActive(false);
        EquippedItem = EquippableItems.Find(i => i.ItemId == id);
        EquippedItem.ItemPrefab.SetActive(true);
        UpdateEquipment?.Invoke();
    }

    public void AddEquippable(EquippableItemData data)
    {
        bool inInventory = EquippableItems.Exists(i => i.ItemId == data.ItemId);
        if (!inInventory)
        {
            EquippableItems.Add(new EquippableItem(data, EquipmentParent));
            UpdateEquipment?.Invoke();
        }
        else
            Debug.Log($"Adventurer already has {data.Name}");
    }

    public void ChangeArrowCount(int change)
    {
        Arrows += change;
        UpdateConsumable?.Invoke();
    }

}


