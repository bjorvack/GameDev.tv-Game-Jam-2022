using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [field: SerializeField] public AudioListener AudioListener { get; private set; }

    [field: SerializeField] public GameObject BackgroundMusic { get; private set; }

    [field: SerializeField] public GameObject JumpSFX { get; private set; }


    void Start()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);

            return;
        }

        DontDestroyOnLoad(gameObject);
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        Object.Instantiate(BackgroundMusic);
    }

    public void PlayJumpSFX()
    {
        PlaySFX(JumpSFX);
    }

    private void PlaySFX(GameObject clip)
    {
        GameObject sfx = Object.Instantiate(clip, AudioListener.transform);
        StartCoroutine(DestroySFX(sfx));
    }

    IEnumerator DestroySFX(GameObject sfx)
    {
        float clipLenght = sfx.GetComponent<AudioSource>().clip.length;

        yield return new WaitForSeconds(clipLenght);
        Destroy(sfx);
    }
}
