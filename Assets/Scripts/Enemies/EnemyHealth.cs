using System.Security.Cryptography;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private int _maxHeath = 1;
    [SerializeField] private AudioSource _damageAudio;
    [SerializeField] private Animator _animator;
    [SerializeField] private Enemy _enemy;
    private int _currentHealth;
    private void Start()
    {
        _currentHealth = _maxHeath;
    }

    public void UpdateHealth(int changeInHealth)
    {
        _currentHealth = ChangeHealth(changeInHealth);
        if (_currentHealth <= 0)
        {
            _animator.SetTrigger("Die");
            _enemy.enabled = false;
            Destroy(gameObject, 5);
        }
    }

    private int ChangeHealth(int amountToChange)
    {
        if (amountToChange < 0)
            _damageAudio.Play();
        return Mathf.Clamp(_currentHealth + amountToChange, 0, _maxHeath);
    }
}