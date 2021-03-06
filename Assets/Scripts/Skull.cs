using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    [field: SerializeField] public float MovementSpeed { get; private set; }

    [field: SerializeField] public float DetectionRadius { get; private set; }

    private Vector3 initialPositon;

    private void Start()
    {
        initialPositon = transform.position;
        FindObjectOfType<GameSession>().ResetEvent += Reset;
    }

    private void Update()
    {
        Follow();
    }

    private void Reset()
    {
        transform.position = initialPositon;
    }

    private void Follow()
    {
        PlayerStateMachine player = FindObjectOfType<PlayerStateMachine>();
        if (player == null) { return; }

        Vector2 targetPosition;
        // If the distance between the player and the skull is less than the detection radius,
        // then the skull will move towards the player.
        if (Vector2.Distance(transform.position, player.transform.position) < DetectionRadius &&
            !(player.CurrentState is PlayerDeathState)
        ) {
            targetPosition = Vector2.MoveTowards(
                transform.position,
                FindObjectOfType<PlayerStateMachine>().transform.position,
                MovementSpeed * Time.deltaTime
            );
        }
        else
        {
            targetPosition = Vector2.MoveTowards(
                transform.position,
                initialPositon,
                MovementSpeed * Time.deltaTime
            );
        }

        FlipSprite(transform.position, targetPosition);
        transform.position = targetPosition;
    }

    private void FlipSprite(Vector2 currentPositon, Vector2 targetPositon)
    {
        if (currentPositon.x == targetPositon.x) { return; }

        if (currentPositon.x < targetPositon.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            StartCoroutine(HurtPlayer(other));
        }
    }

    private void ResetPosition()
    {
        transform.position = initialPositon;
    }

    IEnumerator HurtPlayer(Collider2D other)
    {
        FindObjectOfType<AudioManager>().PlayDeathSFX();
        float clipLenght = FindObjectOfType<AudioManager>().GetDeathSFXLenght() * 2 / 3;
        PlayerStateMachine stateMachine = FindObjectOfType<PlayerStateMachine>();
        
        stateMachine.SwitchState(
            new PlayerDeathState(
                stateMachine
            )
        );

        yield return new WaitForSeconds(clipLenght);

        FindObjectOfType<GameSession>().ProcessPlayerDeath();
        
        stateMachine.SwitchState(
            new PlayerMoveState(
                stateMachine
            )
        );
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectionRadius);
    }
}
