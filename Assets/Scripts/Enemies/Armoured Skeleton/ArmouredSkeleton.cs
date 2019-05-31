using System.Collections.Generic;
using UnityEngine;

public class ArmouredSkeleton : Enemy
{
    [SerializeField] private AudioSource _audioSrc;
    new void Awake()
    {
        base.Awake();
        var initialStates = new List<EnemyBaseState>()
        {
            new ArmouredSkeletonChase(this),
            new ArmouredSkeletonCombat(this)
        };
        StateMachine = new EnemyStateMachine(initialStates);
        _audioSrc.Play();
    }

    private void OnDestroy()
    {
        if (_audioSrc.isPlaying)
            _audioSrc.Stop();
    }
}