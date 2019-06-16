using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<IInventoryItem> _items = new List<IInventoryItem>();

    public IInventoryItem ActiveItem { get; private set; }
    private int _activeItemIndex;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            PreviousItem();
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            NextItem();
        }
        Debug.Log($"Active Item: {ActiveItem}");
    }

    private void NextItem()
    {
        if (_activeItemIndex < _items.Count - 1)
        {
            _activeItemIndex++;
            ActiveItem = _items[_activeItemIndex];
        }
        else
        {
            _activeItemIndex = 0;
            ActiveItem = _items[_activeItemIndex];
        }
    }

    private void PreviousItem()
    {
        if (_activeItemIndex > 0)
        {
            _activeItemIndex--;
            ActiveItem = _items[_activeItemIndex];
        }
        else
        {
            _activeItemIndex = _items.Count - 1;
            ActiveItem = _items[_activeItemIndex];
        }
    }

    public void UseActiveItem()
    {
        if (_items.Count > 0)
        {
            if (ActiveItem == null) ActiveItem = _items[0];
            Debug.Log($"Using {ActiveItem}");
            ActiveItem.Use(this);
        }
        else
        {
            Debug.LogWarning("No Items in Inventory.");
        }
    }

    public void SetActive(IInventoryItem item)
    {
        if (_items.Contains(item))
        {
            ActiveItem = item;
        }
    }

    public void AddItem(IInventoryItem item)
    {
        if (!_items.Contains(item) || !item.Unique)
        {
            _items.Add(item);
        }
        else
        {
            Debug.LogWarning($"You can only carry 1 {item}");
        }
    }

    public void RemoveItem(IInventoryItem item)
    {
        if (_items.Contains(item)) _items.Remove(item);
    }

    private void CheckForNullItem()
    {
        if (_items.Count <= 0) return;
        if (ActiveItem == null)
        {
            _activeItemIndex = 0;
            ActiveItem = _items[_activeItemIndex];

        }
    }
}

public interface IInventoryItem
{
    bool Unique { get; }
    void Use(PlayerInventory inventory);
}
