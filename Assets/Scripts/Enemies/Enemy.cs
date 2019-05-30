using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform Player { get; private set; }
    public Vector3 PlayerLastLocation { get; private set; }
    public EnemyVision Vision { get; private set; }
    public EnemyStateMachine StateMachine { get; private set; }

    public void SetPlayer(Transform player)
    {
        Player = player;
    }

    public void Alert(Vector3 location)
    {
        PlayerLastLocation = location;
    }

    private void Awake()
    {
        Vision = new EnemyVision(transform);
        var initialStates = new List<EnemyBaseState>()
        {
            new StationaryState(this),
            new WanderState(this),
            new ChaseState(this),
            new CombatState(this),
            new AlertState(this)
        };
        StateMachine = new EnemyStateMachine(initialStates);
    }

    void Update()
    {
        StateMachine.UpdateStateMachine();
    }
}

public class EnemyVision
{
    private Transform _transform;

    private float _maxVisionDistance = 20f;
    private Quaternion _startingAngle = Quaternion.AngleAxis(-75, Vector3.up);
    private Quaternion _stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    public EnemyVision(Transform enemyTransform)
    {
        _transform = enemyTransform;
    }

    public Transform UpdateVision()
    {
        RaycastHit hit;
        var angle = _transform.rotation * _startingAngle;
        var direction = angle * Vector3.forward;
        var pos = _transform.position;
        for (int i = 0; i < 24; i++)
        {
            if (Physics.Raycast(pos, direction, out hit, _maxVisionDistance))
            {
                var player = hit.collider.GetComponent<PlayerController>();
                if (player != null)
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    return player.transform;
                }
                Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
            }
            else
            {
                Debug.DrawRay(pos, direction * _maxVisionDistance, Color.white);
            }

            direction = _stepAngle * direction;
        }

        return null;
    }
}

public class EnemyStateMachine
{
    private List<EnemyBaseState> _possibleStates;
    private EnemyBaseState _currentState;

    public EnemyStateMachine(List<EnemyBaseState> possibleStates)
    {
        _possibleStates = possibleStates;
    }

    public void UpdateStateMachine()
    {
        if (_currentState == null)
        {
            _currentState = _possibleStates.First();
        }

        var nextState = _currentState?.UpdateState();
        if (nextState != null
            && nextState != _currentState.GetType())
        {
            ChangeState(nextState);
        }
    }

    private void ChangeState(Type nextState)
    {
        _currentState = _possibleStates.FirstOrDefault(s => s.GetType() == nextState);
    }
}