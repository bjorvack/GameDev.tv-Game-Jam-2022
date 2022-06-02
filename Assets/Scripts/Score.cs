using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [field: SerializeField] public GameObject Souls { get; private set; }
    [field: SerializeField] public TMP_Text ScoreText { get; private set; }

    private int maxSouls;
    private int currentSouls;

    // Start is called before the first frame update
    void Start()
    {
        maxSouls = Souls.GetComponentsInChildren<Soul>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        currentSouls = FindObjectOfType<GameSession>()?.Souls ?? 0;

        ScoreText.text = currentSouls.ToString() + "/" + maxSouls.ToString() + " souls collected";
    }
}
