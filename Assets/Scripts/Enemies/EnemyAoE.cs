using UnityEngine;

public class EnemyAoE : MonoBehaviour
{
    [SerializeField] private int _damage = -1;

    private void OnTriggerStay(Collider other)
    {
        var playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth)
            playerHealth.UpdateHealth(_damage);
    }
}
