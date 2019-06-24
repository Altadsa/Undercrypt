using System.Linq;
using GEV;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] ScriptableEvent _onInventoryOpen;

    [SerializeField] InventorySlot _slotPrefab;

    [SerializeField] Transform _WeaponSlots;
    [SerializeField] Transform _UtilitySlots;
    [SerializeField] Transform _ConsumableSlots;
    [SerializeField] Transform _QuestSlots;

    public static InventoryUI Instance;

    Inventory _inventory;

    private void Awake()
    {
        if (!Instance) Instance = this;
        _inventory = Inventory.Instance;
        _inventory.UpdateEquipment += UpdateEquipment;
        _inventory.UpdateQuestItems += UpdateEquipment;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) _onInventoryOpen.Raise();
    }

    public void UpdateEquipment()
    {
        var weapons = _inventory.EquippableItems.Where(i => i.EquipmentType == EquipmentType.MeleeWeapon).ToArray();
        var utilities = _inventory.EquippableItems.Where(i => i.EquipmentType == EquipmentType.UtilityItem).ToArray();
        var questItems = _inventory.QuestItems.ToArray();
        UpdateSlots(weapons, _WeaponSlots);
        UpdateSlots(utilities, _UtilitySlots);
        UpdateSlots(questItems, _QuestSlots);
    }

    private void UpdateSlots(IItem[] items, Transform slotParent)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (i >= slotParent.childCount)
            {
                var newSlot = Instantiate(_slotPrefab, slotParent);
                newSlot.Initialize(items[i]);
            }
        }
    }

    public void SelectItem(int i)
    {
        _inventory.EquipItem(i);
    }
}