using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    [field: SerializeField] public int Souls { get; private set; }
    private Boolean collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !collected) {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider2D other)
    {
        FindObjectOfType<AudioManager>().PlayPickupSFX();
        float clipLenght = FindObjectOfType<AudioManager>().GetPickupSFXLenght() / 2;
        collected = true;

        // Disable all child spriterenderers
        foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>()) {
            spriteRenderer.enabled = false;
        }

        yield return new WaitForSeconds(clipLenght);

        FindObjectOfType<GameSession>().AddSouls(Souls);
        DestroyImmediate(gameObject);
    }
}
