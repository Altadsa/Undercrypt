
using GEV;
using UnityEngine;
using UnityEngine.Playables;


public class DungeonDoor : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] GameObject _roomA;
    [SerializeField] GameObject _roomB;

    [SerializeField] private ScriptableEvent _onDoorOpened;
    [SerializeField] private ScriptableEvent _onDoorClosed;

    private IDoorCondition _doorCondition;

    private void Awake()
    {
        _doorCondition = GetComponent<IDoorCondition>();
    }
    public void OnDoorOpened()
    {
        _onDoorOpened.Raise();
    }
    public void OnDoorClosed()
    {
        _onDoorClosed.Raise();
    }
    private void ToggleActiveRoom()
    {
        _roomA.SetActive(!_roomA.activeInHierarchy);
        _roomB.SetActive(!_roomB.activeInHierarchy);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (GetComponent<IDoorCondition>() != null)
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
                _animator.SetTrigger("Open");
                ToggleActiveRoom();
            }
        }

    }

}