using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public int Scene;
    public TextMeshProUGUI dialogueText; // Texto na UI para exibir o diálogo.
    public TextMeshProUGUI promptText; // Texto de aviso (e.g., "Pressione Espaço para continuar").
    public string[] dialogueLines; // Lista de falas.
    public float textSpeed = 0.05f; // Velocidade de digitação.

    private int currentLine = 0; // Índice da fala atual.
    private bool isTyping = false; // Indica se o texto está sendo digitado.
    private SceneTransitionManager transitionManager; // Referência para gerenciar transição.
    private AudioSource audioSource;
    private bool jaTocouAudio = false;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        transitionManager = FindObjectOfType<SceneTransitionManager>();
        promptText.gameObject.SetActive(false); // Esconde o aviso inicialmente.
        StartCoroutine(TypeDialogue());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[currentLine];
                isTyping = false;
                ShowPrompt();
            }
            else
            {
                NextDialogue();
            }
        }

        // Verifica a cena atual e a linha atual
        string sceneName = SceneManager.GetActiveScene().name;

        if (!jaTocouAudio &&
            ((sceneName == "Outside Final" && currentLine == 2) ||
            (sceneName == "Outside" && currentLine == 7)))
        {
            if (audioSource != null)
            {
                audioSource.Play();
                jaTocouAudio = true; // Garante que só toca uma vez
            }
            else
            {
                Debug.LogWarning("AudioSource não encontrado!");
            }
        }
    }


    private IEnumerator TypeDialogue()
    {
        isTyping = true;
        promptText.gameObject.SetActive(false); // Esconde o aviso durante a digitação.
        dialogueText.text = ""; // Limpa o texto atual.

        foreach (char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter; // Adiciona cada letra ao texto.
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false; // Finaliza a digitação.
        ShowPrompt(); // Mostra o aviso ao terminar.
    }

    private void ShowPrompt()
    {
        promptText.gameObject.SetActive(true); // Exibe o aviso para pressionar Espaço.
        Debug.Log(currentLine);
    }

    public void NextDialogue()
    {
        if (currentLine < dialogueLines.Length - 1)
        {
            currentLine++;
            StartCoroutine(TypeDialogue());
        }
        else
        {
            // Quando o diálogo termina, troca para a próxima cena.
            SceneManager.LoadScene(Scene); // Substitua pelo nome da cena desejada.
        }
    }
}
