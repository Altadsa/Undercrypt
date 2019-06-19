using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable Objects/Items")]
public class InventoryItemData : ScriptableObject
{
    public Image Icon;
    public GameObject ItemPrefab;
}