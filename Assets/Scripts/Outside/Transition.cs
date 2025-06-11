using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneTransitionManager : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    private void Start()
    {
        StartCoroutine(FadeOut());
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(TransitionToScene(sceneName));
    }

    private IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(0, 1, t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);
    }

    private IEnumerator FadeOut()
    {
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(1, 0, t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
        fadeImage.gameObject.SetActive(false);
    }

    private IEnumerator TransitionToScene(string Inside)
    {
        yield return FadeIn();
        SceneManager.LoadScene(Inside);
    }

    internal void LoadScene(int scene)
    {
        throw new NotImplementedException();
    }
}
