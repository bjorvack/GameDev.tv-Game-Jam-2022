using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FocusChange : MonoBehaviour
{
    [field: SerializeField] public Transform Target { get; private set; }

    [field: SerializeField] public GameObject Camera { get; private set; }

   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            Focus();
            FindObjectOfType<GameSession>().Finish();
        }
    }

    private void Focus()
    {
        Camera.GetComponent<CinemachineVirtualCamera>().m_Follow = Target;
        Camera.GetComponent<CinemachineVirtualCamera>().m_LookAt = Target;

        GameObject.FindWithTag("UI").GetComponent<Canvas>().enabled = false;
    }
}
