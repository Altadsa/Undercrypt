using GEV;
using UnityEngine;

public class DungeonDoor : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] GameObject _roomA;
    [SerializeField] GameObject _roomB;

    [SerializeField] private ScriptableEvent _onDoorOpened;
    [SerializeField] private ScriptableEvent _onDoorClosed;

    private OpenCondition _condition;

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
            if (GetComponent<OpenCondition>())
            {
                _condition = GetComponent<OpenCondition>();
                if (_condition.ConditionsMet())
                {
                    Destroy(_condition);
                    _animator.SetTrigger("Open");
                    ToggleActiveRoom();
                }
                else
                {
                    _condition.ConditionMessage();
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