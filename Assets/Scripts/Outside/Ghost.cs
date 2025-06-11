using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Ghost : MonoBehaviour
{
    public int indexScene;
    public float fadeDuration = 1f; // Tempo total do fade
    public float delayBeforeFade = 5f;
    public SpriteRenderer ghost;

    void Start()
    {
        ghost = GetComponent<SpriteRenderer>();
        if (ghost != null)
        {
            StartCoroutine(DelayedFadeOut());
        }
    }

    private System.Collections.IEnumerator DelayedFadeOut()
    {
        

        // Aguarda o tempo especificado antes de iniciar o fade
        yield return new WaitForSeconds(delayBeforeFade);

        // Inicia o fade
        Color spriteColor = ghost.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            spriteColor.a = alpha;
            ghost.color = spriteColor;
            yield return null;
        }

        // Torna o objeto invisível após o fade
        spriteColor.a = 0f;
        ghost.color = spriteColor;
    }
}
