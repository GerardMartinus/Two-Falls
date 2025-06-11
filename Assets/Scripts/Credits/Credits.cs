using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Credits : MonoBehaviour
{
    public int indexScene;

    void Start()
    {
        StartCoroutine(WaitAndLoadScene(5f)); // Aguarda 5 segundos
    }

    IEnumerator WaitAndLoadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(indexScene);
    }
}
