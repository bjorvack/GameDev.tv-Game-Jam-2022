using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [field: SerializeField] public TMP_Text SoulsText { get; private set; }

    private void Update()
    {
        if (FindObjectOfType<GameSession>() == null) { return; }
        
        SoulsText.text = FindObjectOfType<GameSession>().Souls.ToString();
    }
}
