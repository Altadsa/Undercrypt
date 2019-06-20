using GEV;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] EquippableItemData _data;
    [SerializeField] ScriptableEvent _onItemObtained;

    public void Obtain()
    {
        Inventory.Instance.AddEquippable(_data);
        _onItemObtained.Raise();
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    var player = other.GetComponent<PlayerController>();
    //    if (player)
    //    {
    //        Inventory.Instance.AddEquippable(_data);
    //        gameObject.SetActive(false);
    //    }
    //}
}
