using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    public PlayerDeathState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.GetComponent<SpriteRenderer>().enabled = false;
    }

    public override void Exit()
    {
        stateMachine.GetComponent<SpriteRenderer>().enabled = true;
    }

    public override void Tick(float deltaTime)
    {

    }
}
