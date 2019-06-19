using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerDownHandler
{
    public int _index;
    public InventoryUI _ui;

    private void Awake()
    {
        Debug.Log($"Index {_index}");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Select");
        _ui.SelectItem(_index);
    }
}