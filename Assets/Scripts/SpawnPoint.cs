using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void Start() {
        transform.position = FindObjectOfType<PlayerStateMachine>().transform.position;
    }

    public void Move(Vector3 targetPosition) {
        transform.position = targetPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.4f);
    }
}
