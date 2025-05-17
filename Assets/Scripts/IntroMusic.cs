using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroMusic : MonoBehaviour
{
    public static IntroMusic instance;
    public AudioClip backgroundMusic;
    private AudioSource audioSource;
    public float fadeDuration = 2f; // duração do fade-out

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.volume = 0.5f;
        audioSource.Play();

        // Detectar troca de cena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level1")
        {
            StartCoroutine(FadeOutAndStop());
        }
    }

    IEnumerator FadeOutAndStop()
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
