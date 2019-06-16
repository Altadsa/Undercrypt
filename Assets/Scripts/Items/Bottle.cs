using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour 
{
    [SerializeField] private InventoryItem _item;
    [SerializeField] private int _healthValue = 10;

    private void OnTriggerEnter(Collider other)
    {
        var inventory = other.GetComponentInParent<PlayerInventory>();
        if (inventory)
        {
            inventory.AddItem(_item);
            Destroy(gameObject);
        }
    }

}





