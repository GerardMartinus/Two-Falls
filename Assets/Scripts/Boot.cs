using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Arraste o VideoPlayer aqui
    public float delayBeforeSceneChange = 5f; // Tempo em segundos antes de mudar a cena
    public string nextSceneName = "NomeDaProximaCena"; // Substitua pelo nome da sua cena

    private void Start()
    {
        // Inicia a reprodução do vídeo
        videoPlayer.Play();

        // Começa a corrotina que espera 5 segundos e muda a cena
        StartCoroutine(WaitAndChangeScene());
    }

    private IEnumerator WaitAndChangeScene()
    {
        // Espera o tempo definido
        yield return new WaitForSeconds(delayBeforeSceneChange);

        // Altere a cena
        SceneManager.LoadScene(nextSceneName);
    }
}
