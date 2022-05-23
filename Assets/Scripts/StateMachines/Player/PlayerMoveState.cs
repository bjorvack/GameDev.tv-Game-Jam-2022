using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{    
    public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.InputReader.JumpEvent += Jump;
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= Jump;
    }

    public override void Tick(float deltaTime)
    {
        ProcessMovement(stateMachine.InputReader.MovementValue);
    }
    
    private void ProcessMovement(Vector2 movement)
    {
        stateMachine.GetComponent<Rigidbody2D>().velocity = new Vector2(
            stateMachine.MovementSpeed * Mathf.Sign(movement.x),
            stateMachine.GetComponent<Rigidbody2D>().velocity.y
        );
    }

    private void Jump()
    {
        if (!stateMachine.GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        
        stateMachine.GetComponent<Rigidbody2D>().velocity = new Vector2(
            stateMachine.GetComponent<Rigidbody2D>().velocity.x,
            stateMachine.JumpSpeed
        );
    }
}
