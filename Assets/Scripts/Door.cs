using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [field: SerializeField] public Transform Target { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            StartCoroutine(Teleport(other));
        }
    }

    IEnumerator Teleport(Collider2D other)
    {
        FindObjectOfType<AudioManager>().PlayWarpSFX();
        float clipLenght = FindObjectOfType<AudioManager>().GetWarpSFXLenght() / 2;

        yield return new WaitForSeconds(clipLenght);

        other.transform.position = Target.position;
        FindObjectOfType<SpawnPoint>().Move(Target.position);
    }
}
