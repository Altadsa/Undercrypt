using GEV;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] ScriptableEvent _onInventoryOpen;

    [SerializeField] InventorySlot _slotPrefab;

    [SerializeField] Transform _WeaponSlots;
    [SerializeField] Transform _ActionSlots;
    [SerializeField] Transform _ConsumableSlots;

    public static InventoryUI Instance;

    Inventory _inventory;

    private void Start()
    {
        if (!Instance) Instance = this;
        _inventory = Inventory.Instance;
        _inventory.UpdateEquipment += UpdateEquipment;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) _onInventoryOpen.Raise();
    }

    public void UpdateEquipment()
    {
        var weapons = _inventory.EquippableItems.ToArray();
        UpdateSlots(weapons, _WeaponSlots);
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