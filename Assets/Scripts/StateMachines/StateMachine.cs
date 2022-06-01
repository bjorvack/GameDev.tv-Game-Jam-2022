using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public State CurrentState { get; private set; }

    void Update()
    {
        CurrentState?.Tick(Time.deltaTime);
    }

    public void SwitchState(State state)
    {
        CurrentState?.Exit();
        CurrentState = state;
        CurrentState?.Enter();
    }
}
