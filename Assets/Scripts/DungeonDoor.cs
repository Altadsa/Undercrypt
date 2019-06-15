using UnityEngine;


public class DungeonDoor : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player)
        {
            _animator.SetTrigger("Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player)
        {
            _animator.SetTrigger("Close");
        }
    }
}
