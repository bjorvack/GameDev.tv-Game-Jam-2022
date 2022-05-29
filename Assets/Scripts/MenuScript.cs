using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [field: SerializeField] public int GameScene { get; private set; }

    public void StartGame()
    {
        SceneManager.LoadScene(GameScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
