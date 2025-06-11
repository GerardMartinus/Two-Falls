using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SplashScreenController : MonoBehaviour
{
    public CanvasGroup fadePanel; // Referência ao painel de fade
    public VideoPlayer videoPlayer; // Referência ao VideoPlayer
    public float fadeDuration = 1f; // Duração do fade

    private void Start()
    {
        StartCoroutine(PlaySplashScreen());
    }

    private IEnumerator PlaySplashScreen()
    {
        // Fade In
        yield return StartCoroutine(Fade(1, 0));

        // Reproduzir o vídeo
        videoPlayer.Play();
        yield return new WaitUntil(() => videoPlayer.isPrepared);
        yield return new WaitUntil(() => videoPlayer.time >= videoPlayer.length);

        // Fade Out
        yield return StartCoroutine(Fade(0, 1));

        // Trocar para a próxima cena
        SceneManager.LoadScene("Menu"); // Substitua pelo nome da próxima cena
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadePanel.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            yield return null;
        }
        fadePanel.alpha = endAlpha;
    }
}
