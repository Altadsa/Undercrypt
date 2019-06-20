using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public Transform EquipmentParent;

    public int Arrows { get; private set; } = 10;
    public int Keys { get; private set; } = 1;

    public List<IEquippableItem> EquippableItems { get; private set; } = new List<IEquippableItem>();
    public List<IConsumableItem> ConsumableItems { get; private set; } = new List<IConsumableItem>();

    public IEquippableItem EquippedWeapon = null;
    public IEquippableItem EquippedUtility = null;

    public event Action UpdateEquipment;
    public event Action UpdateConsumable; 


    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    private void Start()
    {
        UpdateEquipment?.Invoke();
        UpdateConsumable?.Invoke();
    }

    public void EquipItem(int id)
    {
        var itemToEquip = EquippableItems.Find(i => i.ItemId == id);
        switch (itemToEquip.EquipmentType)
        {
            case EquipmentType.MeleeWeapon:
                EquipWeapon(itemToEquip);
                break;
            case EquipmentType.UtilityItem:
                EquipUtility(itemToEquip);
                break;
        }
        Debug.Log($"Equipped Weapon {EquippedWeapon?.Name}...Equipped Utility {EquippedUtility?.Name}");
        UpdateEquipment?.Invoke();
    }

    private void EquipUtility(IEquippableItem itemToEquip)
    {
        EquippedWeapon?.ItemPrefab.SetActive(false);
        EquippedUtility?.ItemPrefab.SetActive(false);
        EquippedUtility = itemToEquip;
        EquippedUtility.ItemPrefab.SetActive(true);
    }

    private void EquipWeapon(IEquippableItem itemToEquip)
    {
        EquippedUtility?.ItemPrefab.SetActive(false);
        EquippedWeapon?.ItemPrefab.SetActive(false);
        EquippedWeapon = itemToEquip;
        EquippedWeapon.ItemPrefab.SetActive(true);
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

    public void ChangeKeyCount(int change)
    {
        Keys += change;
        UpdateConsumable?.Invoke();
    }

}


