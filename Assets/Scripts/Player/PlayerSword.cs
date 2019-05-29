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
        if (other.GetComponent<EnemyHealth>())
        {
            other.GetComponent<EnemyHealth>().UpdateHealth(-1);
        }
    }
}
