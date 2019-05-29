using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUi : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    [SerializeField]
    private TMP_Text _healthText;

    [SerializeField] private Image _healthBar;

    private void Awake()
    {
        _playerHealth = FindObjectOfType<PlayerHealth>();
        _playerHealth.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int current, int max)
    {
        _healthText.text = $"{current}/{max}";
        _healthBar.fillAmount = current / (float) max;
    }

}
