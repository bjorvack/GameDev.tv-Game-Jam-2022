using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    private Transform spawnPoint;

    public PlayerDeathState(PlayerStateMachine stateMachine, Transform spawnPoint) : base(stateMachine)
    {
        this.spawnPoint = spawnPoint;
    }

    public override void Enter()
    {
        stateMachine.GetComponent<SpriteRenderer>().enabled = false;
        stateMachine.transform.position = spawnPoint.position;
    }

    public override void Exit()
    {
        stateMachine.GetComponent<SpriteRenderer>().enabled = true;
    }

    public override void Tick(float deltaTime)
    {

    }
}
