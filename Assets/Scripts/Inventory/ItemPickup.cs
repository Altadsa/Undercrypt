using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] EquippableItemData _data;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player)
        {
            Inventory.Instance.AddEquippable(_data);
            gameObject.SetActive(false);
        }
    }
}
