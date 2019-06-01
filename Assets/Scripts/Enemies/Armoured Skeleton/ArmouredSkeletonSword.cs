using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmouredSkeletonSword : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        var playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth)
        {
            playerHealth.UpdateHealth(-2);
        }
    }
}
