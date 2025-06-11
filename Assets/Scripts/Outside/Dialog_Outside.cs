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
                // Completa o texto instantaneamente se está digitando.
                StopAllCoroutines();
                dialogueText.text = dialogueLines[currentLine];
                isTyping = false;
                ShowPrompt(); // Mostra o aviso para avançar.
            }
            else
            {
                NextDialogue(); // Avança para a próxima linha.
            }
        }

        if (currentLine == 1)
        {
        if (audioSource != null)
            {
            audioSource.Play();
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
