using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public event Action<int, int> OnHealthChanged;
    private float _damageCd = 1, _timeSinceDamaged = 0;
    private int _currentHealth, _maxHealth;

    private bool Damageable => _timeSinceDamaged >= _damageCd;

    private void Start()
    {
        _maxHealth = 80;
        _currentHealth = 80;
        OnHealthChanged?.Invoke(_currentHealth,_maxHealth);
        _timeSinceDamaged = _damageCd;
    }

    public void UpdateHealth(int changeInHealth)
    {
        if (changeInHealth < 0 && Damageable)
        {
            StartCoroutine(DamageCooldown());
            _currentHealth = ChangeHealth(changeInHealth);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
        else
        {
            _currentHealth = ChangeHealth(changeInHealth);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }

    private int ChangeHealth(int changeInHealth)
    {
        return Mathf.Clamp(_currentHealth + changeInHealth, 0, _maxHealth);
    }

    IEnumerator DamageCooldown()
    {
        _timeSinceDamaged = 0;
        while (!Damageable)
        {
            _timeSinceDamaged += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}