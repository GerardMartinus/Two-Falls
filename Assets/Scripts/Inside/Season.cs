using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeasonManager : MonoBehaviour
{
    private DialogManager dialogManager;

    [Header("Season Configuration")]
    public Sprite[] seasonBackgrounds; // Em ordem: Primavera, Verão, Outono, Inverno
    public string[] seasonNames = { "Primavera", "Verão", "Outono", "Inverno" };

    [Header("UI References")]
    public Image backgroundImage;
    public Image fadeOverlay;
    public TextMeshProUGUI seasonText;

    [Header("Transition Settings")]
    public float fadeDuration = 1f;
    public float displayDuration = 2f;

    private int currentSeasonIndex = 0;

    [Header("Audio")]
    public AudioSource autumnAudio;
    public AudioSource winterAudio;
    public AudioSource springAudio;
    public AudioSource summerAudio;
    public AudioSource autumnAudio2;

    void Start()
    {
        // Subscreve ao evento de troca de estação
        DiaManager.OnTrocaEstacao += HandleSeasonChange;

        // Configura os áudios (garantia de estarem carregados)
        autumnAudio?.GetComponent<AudioSource>();
        winterAudio?.GetComponent<AudioSource>();
        springAudio?.GetComponent<AudioSource>();
        summerAudio?.GetComponent<AudioSource>();
        autumnAudio2?.GetComponent<AudioSource>();

        // Carrega o OrderManager e DialogManager
        dialogManager = FindObjectOfType<DialogManager>();

        if (dialogManager == null)
        {
            Debug.LogError("DialogManager não encontrado!");
        }

        // Define o primeiro background
        if (seasonBackgrounds.Length > 0)
        {
            backgroundImage.sprite = seasonBackgrounds[0];
        }

        // Esconde elementos visuais inicialmente
        fadeOverlay.color = new Color(0, 0, 0, 0);
        seasonText.gameObject.SetActive(false);

        // Inicia com áudio de outono
        if (currentSeasonIndex == 0)
        {
            autumnAudio?.Play();
        }
    }

    void HandleSeasonChange(string nomeEstacao, int index)
    {
        StartCoroutine(ChangeSeason(index));
    }

    IEnumerator ChangeSeason(int index)
    {
        // Fade in
        yield return StartCoroutine(FadeScreen(true));

        // Desativa diálogos durante a transição
        if (dialogManager != null)
        {
            dialogManager.gameObject.SetActive(false);
        }

        currentSeasonIndex = index;

        // Troca de música por estação
        switch (currentSeasonIndex)
        {
            case 1:
                autumnAudio?.Stop();
                winterAudio?.Play();
                break;
            case 2:
                winterAudio?.Stop();
                springAudio?.Play();
                break;
            case 3:
                springAudio?.Stop();
                summerAudio?.Play();
                break;
            case 4:
                summerAudio?.Stop();
                autumnAudio2?.Play();
                break;
        }

        // Se chegou ao último outono novamente, termina o jogo
        if (currentSeasonIndex == 0 && index != 0)
        {
            SceneManager.LoadScene("Outside Final");
            yield break;
        }

        // Mostra nome da estação
        seasonText.text = seasonNames[currentSeasonIndex % seasonNames.Length];
        seasonText.gameObject.SetActive(true);

        yield return new WaitForSeconds(displayDuration);

        seasonText.gameObject.SetActive(false);

        if (dialogManager != null)
        {
            dialogManager.gameObject.SetActive(true);
            dialogManager.StartSeasonDialog(currentSeasonIndex % seasonNames.Length);
        }

        // Atualiza background
        backgroundImage.sprite = seasonBackgrounds[currentSeasonIndex % seasonBackgrounds.Length];

        // Fade out
        yield return StartCoroutine(FadeScreen(false));
    }

    IEnumerator FadeScreen(bool fadeToBlack)
    {
        float elapsedTime = 0f;
        Color startColor = fadeToBlack ? new Color(0, 0, 0, 0) : new Color(0, 0, 0, 1);
        Color endColor = fadeToBlack ? new Color(0, 0, 0, 1) : new Color(0, 0, 0, 0);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = elapsedTime / fadeDuration;
            fadeOverlay.color = Color.Lerp(startColor, endColor, normalizedTime);
            yield return null;
        }

        fadeOverlay.color = endColor;
    }
}
