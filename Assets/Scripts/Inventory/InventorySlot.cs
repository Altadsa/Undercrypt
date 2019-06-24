using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    IItem _itemInSlot;
    InventoryUI _inventoryUi;
    public void Initialize(IItem item)
    {
        _itemInSlot = item;
        _inventoryUi = InventoryUI.Instance;
        GetComponent<Image>().sprite = _itemInSlot.Icon;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Select");

        _inventoryUi.SelectItem(_itemInSlot.ItemId);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryItemInfo.Instance.ShowInfo(_itemInSlot);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryItemInfo.Instance.ClearInfo();
    }
}