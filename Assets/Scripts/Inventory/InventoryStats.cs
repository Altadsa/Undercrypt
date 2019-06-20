using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryStats : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] Image _healthBar;

    [SerializeField] private TMP_Text _manaText;
    [SerializeField] Image _manaBar;
    [SerializeField] TMP_Text _arrowInfo;
    [SerializeField] TMP_Text _keyInfo;

    private Inventory _inventory;

    private void Awake()
    {
        FindObjectOfType<PlayerHealth>().OnHealthChanged += UpdateHealth;
        _inventory = Inventory.Instance;
        _inventory.UpdateConsumable += UpdateConsumable;
    }

    public void UpdateHealth(int current, int max)
    {
        _healthText.text = $"HP: {current}/{max}";
        _healthBar.fillAmount = (float) current / max;
    }

    public void UpdateMana(int current, int max)
    {
        _manaText.text = $"MP: {current}/{max}";
        _manaBar.fillAmount = (float) current / max;
    }

    public void UpdateConsumable()
    {
        _arrowInfo.text = _inventory.Arrows.ToString();
        _keyInfo.text = _inventory.Keys.ToString();
    }
}
