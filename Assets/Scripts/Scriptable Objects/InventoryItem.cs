using UnityEngine;

public abstract class InventoryItem : ScriptableObject, IInventoryItem
{
    [SerializeField] private bool _isUnique;
    public bool Unique => _isUnique;
    public abstract void Use(PlayerInventory inventory);
}