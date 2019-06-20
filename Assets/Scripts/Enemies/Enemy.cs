using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, ITargetable
{
    public Transform Transform => transform;

    public Transform Player { get; private set; }
    public Vector3 PlayerLastLocation { get; private set; }
    public EnemyVision Vision { get; private set; }
    public EnemyStateMachine StateMachine { get; protected set; }

    public Transform AreasOfEffect;

    public void SetPlayer(Transform player)
    {
        Player = player;
    }

    public void Alert(Vector3 location)
    {
        PlayerLastLocation = location;
    }

    protected void Awake()
    {
        Vision = new EnemyVision(transform);

    }

    void Update()
    {
        StateMachine.UpdateStateMachine();
    }


}