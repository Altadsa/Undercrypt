using System.Security.Cryptography;
using UnityEngine;


public class DungeonDoor : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] DungeonRoom _roomA;
    [SerializeField] DungeonRoom _roomB;

    private IDoorCondition _doorCondition;

    private void Awake()
    {
        _doorCondition = GetComponent<IDoorCondition>();
    }

    public void ToggleActiveRoom()
    {
        _roomA.ToggleRoom();
        _roomB.ToggleRoom();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (_doorCondition != null)
            {
                if (_doorCondition.ConditionsMet())
                {
                    Destroy(_doorCondition.DoorCondition);
                    _animator.SetTrigger("Open");
                    ToggleActiveRoom();
                }
                else
                {
                    _doorCondition.ConditionMessage();
                }
            }
            else
            {
                ToggleActiveRoom();
            }
        }

    }

}