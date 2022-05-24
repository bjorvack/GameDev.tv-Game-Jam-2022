using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    [field: SerializeField] public int Souls { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            FindObjectsOfType<GameSession>()[0].AddSouls(Souls);
            Destroy(gameObject);
        }
    }
}
