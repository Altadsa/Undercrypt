using System.Collections;
using UnityEngine;

public class EnemyAoE : MonoBehaviour
{
    [SerializeField] private bool _canDespawn;
    [SerializeField] private float _despawnTime = 7;
    [SerializeField] private int _damage = -1;

    private float _timeSpawned = 0;

    private void Start()
    {
        if (_canDespawn)
        {
            StartCoroutine(Despawn());
        }
    }

    private IEnumerator Despawn()
    {
        while (_timeSpawned < _despawnTime)
        {
            _timeSpawned += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(transform.parent.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        var playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth)
            playerHealth.UpdateHealth(_damage);
    }
}

