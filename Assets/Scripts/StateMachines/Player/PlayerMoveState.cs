using System;
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
        FlipSprite(stateMachine.InputReader.MovementValue);
        ProcessMovement(stateMachine.InputReader.MovementValue, deltaTime);
    }

    private void FlipSprite(Vector2 movement)
    {
        if (movement.x == 0f) { return; }

        stateMachine.transform.localScale = new Vector3(Mathf.Sign(movement.x), 1, 1);
    }
    
    private void ProcessMovement(Vector2 movement, float deltaTime)
    {
        if (movement.x == 0f) { return; }

        Vector2 feetPosition = new Vector2(
            stateMachine.transform.position.x,
            stateMachine.transform.position.y - (stateMachine.GetComponent<CapsuleCollider2D>().size.y / 2f)
        );
        float distance = stateMachine.MovementSpeed * deltaTime + (stateMachine.GetComponent<CapsuleCollider2D>().size.x / 2f);

        // Draw raycasts to check if we can move
        RaycastHit2D[] hits = Physics2D.RaycastAll(feetPosition, new Vector2(Mathf.Sign(movement.x), 0f), distance);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) { return; }
        }
        
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
