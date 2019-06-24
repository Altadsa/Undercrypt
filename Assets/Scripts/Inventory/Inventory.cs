using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : SingletonMonoBehaviour<Inventory>
{
    public Transform EquipmentParent;

    public List<EquippableItemData> StartEquipment;

    public List<QuestItemData> StartQuestItems;

    public int Arrows { get; private set; } = 10;
    public int Keys { get; private set; } = 1;

    public List<IEquippableItem> EquippableItems { get; private set; } = new List<IEquippableItem>();

    public List<IItem> QuestItems { get; private set; } = new List<IItem>();
    public List<IConsumableItem> ConsumableItems { get; private set; } = new List<IConsumableItem>();

    public IEquippableItem EquippedWeapon = null;
    public IEquippableItem EquippedUtility = null;

    public event Action UpdateEquipment;
    public event Action UpdateConsumable;
    public event Action UpdateQuestItems;

    private void Awake()
    {
        if (StartEquipment.Count > 0)
        {
            StartEquipment.ForEach(AddEquippable);
        }

        if (StartQuestItems.Count > 0)
        {
            StartQuestItems.ForEach(AddQuestItem);
        }
    }

    private void Start()
    {
        UpdateEquipment?.Invoke();
        UpdateConsumable?.Invoke();
        UpdateQuestItems?.Invoke();
    }

    public void EquipItem(int id)
    {
        var itemToEquip = EquippableItems.Find(i => i.ItemId == id);
        if (itemToEquip == null)
        {
            Debug.LogWarning($"No such item exists.");
            return;
        }

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

    public void AddQuestItem(QuestItemData itemData)
    {
        QuestItems.Add(new QuestItem(itemData));
        UpdateQuestItems?.Invoke();
    }

    public void RemoveQuestItem(int itemId)
    {
        var itemToRemove = QuestItems.Find(i => i.ItemId == itemId);
        QuestItems.Remove(itemToRemove);
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

public class QuestItem : IItem
{
    public int ItemId { get; private set;}
    public Sprite Icon { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public QuestItem(QuestItemData data)
    {
        ItemId = data.ItemId;
        Icon = data.Icon;
        Name = data.Name;
        Description = data.Description;
    }
}


