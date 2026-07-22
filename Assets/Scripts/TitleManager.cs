using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [Header("Button Sound")]
    public AudioSource uiAudioSource;
    public AudioClip clickSound;

    [Range(0f, 1f)]
    public float clickVolume = 1f;

    public float sceneChangeDelay = 0.2f;

    private bool isStarting;

    private void Update()
    {
        if (isStarting)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            PlayGame();
        }
    }

    public void PlayGame()
    {
        if (isStarting)
        {
            return;
        }

        StartCoroutine(PlayGameCoroutine());
    }

    private IEnumerator PlayGameCoroutine()
    {
        isStarting = true;

        if (uiAudioSource != null && clickSound != null)
        {
            uiAudioSource.PlayOneShot(
                clickSound,
                clickVolume
            );
        }

        yield return new WaitForSecondsRealtime(sceneChangeDelay);

        SceneManager.LoadScene("Stage1");
    }
}
