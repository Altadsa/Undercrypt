using UnityEngine;

public class ShadowProjectile : MonoBehaviour
{
    [SerializeField] private GameObject _aoe;
    [SerializeField] private Rigidbody _rigidbody;
    private void Start()
    {
        _rigidbody.AddForce(transform.forward * Random.Range(7.5f, 20f), ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        int layer = LayerMask.GetMask("Ground");
        if (1 << other.gameObject.layer == layer)
        {
            Instantiate(_aoe, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
