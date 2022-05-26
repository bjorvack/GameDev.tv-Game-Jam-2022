using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [field: SerializeField] public GameObject BackgroundMusic { get; private set; }

    [field: SerializeField] public GameObject JumpSFX { get; private set; }

    [field: SerializeField] public GameObject WarpSFX { get; private set; }

    [field: SerializeField] public GameObject DeathSFX { get; private set; }


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
        GameObject music = Object.Instantiate(BackgroundMusic);
        music.transform.parent = gameObject.transform;
    }

    public void PlayJumpSFX()
    {
        PlaySFX(JumpSFX);
    }

    public void PlayWarpSFX()
    {
        PlaySFX(WarpSFX);
    }

    public float GetWarpSFXLenght()
    {
        return GetSFXLength(WarpSFX);
    }

    public void PlayDeathSFX()
    {
        PlaySFX(DeathSFX);
    }

    public float GetDeathSFXLenght()
    {
        return GetSFXLength(DeathSFX);
    }

    private float GetSFXLength(GameObject sfx)
    {
        AudioSource audioSource = sfx.GetComponent<AudioSource>();
        return audioSource.clip.length;
    }

    private void PlaySFX(GameObject clip)
    {
        GameObject sfx = Object.Instantiate(clip);
        StartCoroutine(DestroySFX(sfx));
    }

    IEnumerator DestroySFX(GameObject sfx)
    {
        float clipLenght = sfx.GetComponent<AudioSource>().clip.length;

        yield return new WaitForSeconds(clipLenght);
        Destroy(sfx);
    }
}
