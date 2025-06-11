using UnityEngine;
using UnityEngine.Video;

public class BootScreenController : MonoBehaviour
{
    public Animator bootAnimator; // Referência ao Animator do painel inicial
    public VideoPlayer videoPlayer; // Referência ao componente VideoPlayer
    public string videoUrl; // URL do vídeo a ser reproduzido

    private bool hasStarted = false; // Garante que o comando seja executado uma única vez

    void Update()
    {
        if (!hasStarted && Input.anyKeyDown) // Detecta qualquer tecla
        {
            hasStarted = true; // Evita múltiplas ativações
            StartTransition();
        }
    }

    void StartTransition()
    {
        bootAnimator.SetTrigger("StartGame"); // Aciona a animação
        videoPlayer.url = videoUrl; // Define a URL do vídeo
        videoPlayer.gameObject.SetActive(true); // Ativa o vídeo
        videoPlayer.Play(); // Reproduz o vídeo
    }
}