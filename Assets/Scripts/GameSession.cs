using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{

    [field: SerializeField] public int Lives { get; private set; }

    [field: SerializeField] public int Souls { get; private set; }

    [field: SerializeField] public Transform GameOverPosition { get; private set; }

    [field: SerializeField] public Transform SpawnPointPosition { get; private set; }

    public event Action ResetEvent;

    private Boolean finished = false;

    void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1) {
            Destroy(gameObject);

            return;
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddSouls(int amount)
    {
        Souls += amount;
    }


    public void TakeLife()
    {
        Lives--;
        GameObject lives = GameObject.FindWithTag("Lives");
        lives.GetComponent<RectTransform>().sizeDelta = new Vector2(
            100f * Lives,
            100f
        );
    }

    public void ProcessPlayerDeath()
    {
        TakeLife();

        ResetEvent?.Invoke();

        if (Lives <= 0) {
            GameObject.FindWithTag("Player").transform.position = GameOverPosition.position;

            return;
        }

        GameObject.FindWithTag("Player").transform.position = SpawnPointPosition.position;
    }

    public void Finish()
    {
        finished = true;
    }

    public Boolean IsFinished()
    {
        return finished;
    }
}
