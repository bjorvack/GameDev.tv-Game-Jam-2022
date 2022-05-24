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
    }

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        Vector2 targetPosition;
        // If the distance between the player and the skull is less than the detection radius,
        // then the skull will move towards the player.
        if (Vector2.Distance(transform.position, FindObjectOfType<PlayerStateMachine>().transform.position) < DetectionRadius)
        {
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
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectionRadius);
    }
}
