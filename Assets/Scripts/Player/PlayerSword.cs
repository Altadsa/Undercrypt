using System.Collections;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    [SerializeField] private Collider _swordCollider;
    [SerializeField] int _damage;
    
    private AudioSource _audioSrc;

    private void Start()
    {
        _swordCollider.enabled = false;
        _audioSrc = GetComponent<AudioSource>();
    }

    public void Attack()
    {
        StartCoroutine(SwordAttack());
    }

    IEnumerator SwordAttack()
    {
        if (!_swordCollider.enabled) _swordCollider.enabled = true;
        yield return new WaitForSeconds(1);
        _swordCollider.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        var enemyHealth = collision.gameObject.GetComponentInParent<EnemyHealth>();
        if (enemyHealth)
        {
            _audioSrc.Play();
            enemyHealth.UpdateHealth(-_damage);
        }
    }
}
