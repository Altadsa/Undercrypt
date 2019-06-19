using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    public float HealthRemaining => (float) _currentHealth /  _maxHeath;

    [SerializeField] private int _maxHeath = 1;
    [SerializeField] private AudioSource _damageAudio;
    [SerializeField] private Animator _animator;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private int _currentHealth;

    [SerializeField] private SkinnedMeshRenderer _mesh;
    [SerializeField] private Color _damageColor;

    private Color _normalColor;

    private void Start()
    {
        _currentHealth = _maxHeath;
        _normalColor = _mesh.material.color;
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
        else
            StartCoroutine(DamageCooldown());
    }

    private int ChangeHealth(int amountToChange)
    {
        if (amountToChange < 0)
            _damageAudio.Play();
        return Mathf.Clamp(_currentHealth + amountToChange, 0, _maxHeath);
    }

    IEnumerator DamageCooldown()
    {
        _mesh.material.color = _damageColor;
        yield return new WaitForSeconds(0.5f);
        _mesh.material.color = _normalColor;
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}