using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    private int _currentHealth, _maxHealth;

    public event Action<int, int> OnHealthChanged;

    private void Start()
    {
        _maxHealth = 40;
        _currentHealth = _maxHealth;
        OnHealthChanged?.Invoke(_currentHealth,_maxHealth);
    }

    public void UpdateHealth(int changeInHealth)
    {
        _currentHealth = ChangeHealth(changeInHealth);
        OnHealthChanged?.Invoke(_currentHealth,_maxHealth);
    }

    private int ChangeHealth(int changeInHealth)
    {
        return Mathf.Clamp(_currentHealth + changeInHealth, 0, _maxHealth);
    }
}