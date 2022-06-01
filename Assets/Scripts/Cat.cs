using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [field: SerializeField] public float MovementSpeed { get; private set; }

    [field: SerializeField] public Transform[] Waypoints { get; private set; }

    private int currentWaypointIndex = 0;

    private void Start()
    {
        FindObjectOfType<GameSession>().ResetEvent += Reset;
    }

    private void Reset()
    {
        transform.position = Waypoints[0].transform.position;
    }

    private void Update()
    {

        // If distance to current waypoint if 0.1f or less, then move to next waypoint.
        if (Vector2.Distance(transform.position, Waypoints[currentWaypointIndex].position) <= 0.1f)
        {
            currentWaypointIndex++;
        }

        if (currentWaypointIndex >= Waypoints.Length) {
            currentWaypointIndex = 0;
        }

        // Move towards current waypoint.
        transform.position = Vector2.MoveTowards(
            transform.position,
            Waypoints[currentWaypointIndex].position,
            MovementSpeed * Time.deltaTime
        );

        if (Waypoints[currentWaypointIndex].position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(HurtPlayer(other));
        }
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
}
