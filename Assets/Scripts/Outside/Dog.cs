using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Dog : MonoBehaviour
{
    public float delayBeforeCry = 60f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        if (audioSource != null)
        {
            StartCoroutine(DelayedFadeCry());
        }
    }

    private System.Collections.IEnumerator DelayedFadeCry()
    {
        // Aguarda o tempo especificado antes de iniciar o fade
        yield return new WaitForSeconds(delayBeforeCry);

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
