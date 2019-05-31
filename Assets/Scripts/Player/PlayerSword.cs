using System.Collections;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    [SerializeField] private Collider _swordCollider;

    private void Start()
    {
        _swordCollider.enabled = false;
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

    private void OnTriggerEnter(Collider other)
    {
        var enemyHealth = other.GetComponentInParent<EnemyHealth>();
        if (enemyHealth)
        {
            enemyHealth.UpdateHealth(-1);
        }
    }
}
