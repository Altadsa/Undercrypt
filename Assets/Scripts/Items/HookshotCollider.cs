using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookshotCollider : MonoBehaviour
{

    [SerializeField] private LayerMask _attachabeLayer;
    [SerializeField] private Transform _plTransform;
    [SerializeField] private float speed;

    private bool _pullPlayer = false;

    private void Update()
    {
        if (_pullPlayer)
        {
            _plTransform.position = Vector3.Lerp(_plTransform.position, transform.position, Time.deltaTime * speed);
            if (Vector3.Distance(_plTransform.position, transform.position) < 2)
                _pullPlayer = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Attach Layer {_attachabeLayer.value} Coll Layer {collision.gameObject.layer}");
        if (1<<collision.gameObject.layer ==  _attachabeLayer.value)
        {
            GetComponent<Collider>().enabled = false;
            transform.position = collision.transform.position;
            _pullPlayer = true;
        }
    }

}
