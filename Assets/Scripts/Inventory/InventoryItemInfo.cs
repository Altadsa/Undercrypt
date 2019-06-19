using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryItemInfo : MonoBehaviour
{
    [SerializeField] TMP_Text _nameText;
    [SerializeField] TMP_Text _descText;

    public static InventoryItemInfo Instance;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    public void ShowInfo(IItem item)
    {
        _nameText.text = item.Name;
        _descText.text = item.Description;
    }

    public void ClearInfo()
    {
        _nameText.text = "";
        _descText.text = "";
    }

}
