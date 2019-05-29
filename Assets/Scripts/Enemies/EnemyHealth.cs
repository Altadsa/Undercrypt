using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private int _maxHeath = 1;
    [SerializeField] private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHeath;
    }

    public void UpdateHealth(int changeInHealth)
    {
        _currentHealth = ChangeHealth(changeInHealth);
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private int ChangeHealth(int amountToChange)
    {
        return Mathf.Clamp(_currentHealth + amountToChange, 0, _maxHeath);
    }
}