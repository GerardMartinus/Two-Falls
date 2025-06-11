using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogUI : MonoBehaviour
{
    [Header("Referências UI")]
    [SerializeField] private CanvasGroup dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogText;
    
    [Header("Configurações de Animação")]
    [SerializeField] private float fadeInDuration = 0.5f;
    [SerializeField] private float fadeOutDuration = 0.5f;
    
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        if (dialogPanel != null)
        {
            dialogPanel.alpha = 0f;
            dialogPanel.gameObject.SetActive(false);
        }
    }

    public void ShowDialog(string message)
    {
        if (dialogPanel == null || dialogText == null) return;

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        dialogText.text = message;
        dialogPanel.gameObject.SetActive(true);
        fadeCoroutine = StartCoroutine(FadeDialog(0f, 1f, fadeInDuration));
    }

    public void HideDialog()
    {
        if (dialogPanel == null) return;

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(FadeDialog(1f, 0f, fadeOutDuration));
    }

    private IEnumerator FadeDialog(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        dialogPanel.alpha = startAlpha;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float currentAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            dialogPanel.alpha = currentAlpha;
            yield return null;
        }

        dialogPanel.alpha = endAlpha;

        if (endAlpha == 0f)
        {
            dialogPanel.gameObject.SetActive(false);
        }
    }
}