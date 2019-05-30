using System;
using System.Collections.Generic;
using System.Linq;

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