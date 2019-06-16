using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Items/Health Potion")]
public class HealthPotionItem : InventoryItem
{
    [SerializeField] private int _healValue = 10;

    public override void Use(PlayerInventory inventory)
    {
        Object.FindObjectOfType<PlayerHealth>().UpdateHealth(_healValue);
        inventory.RemoveItem(this);
    }
}

[CreateAssetMenu(menuName = "Scriptable Objects/Items/Grappling Hook")]
public class GrapplingHookItem : InventoryItem
{
    public override void Use(PlayerInventory inventory)
    {
        Transform player = FindObjectOfType<PlayerController>().transform;
        player.transform.position = Camera.main.transform.forward * 10;
    }
}