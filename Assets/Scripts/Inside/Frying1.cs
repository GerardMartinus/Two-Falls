using UnityEngine;
using UnityEngine.UI;

public class PotatoCooking : MonoBehaviour
{
    public Sprite friedPotatoSprite;  // O sprite do hambúrguer frito
    public string newTag;
    public GameObject smokeEffect;         // Objeto de efeito de fumaça com animação
    private bool isOnFryer = false;    // Verifica se está na chapa
    private float cookingTime = 10f;   // Tempo necessário para fritar o hambúrguer
    private float timer = 0f;          // Timer para a contagem regressiva
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    void Start()
    {
        // Obtém o SpriteRenderer do hambúrguer
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.tag = "Batata";
        audioSource = GetComponent<AudioSource>();

        if (smokeEffect != null)
        {
            smokeEffect.SetActive(false);
        }
    }

    void Update()
    {
        // Se o hambúrguer estiver na chapa, inicia a contagem
        if (isOnFryer)
        {  
            Debug.Log("Ta na fritadeira");
            timer += Time.deltaTime;
            if (timer >= cookingTime)
            {
                // Troca o sprite para o hambúrguer frito após o tempo de cozimento
                spriteRenderer.sprite = friedPotatoSprite;
                isOnFryer = false;  // Reseta para evitar a contagem extra
                gameObject.tag = newTag;

                if (audioSource != null)
                {
                    audioSource.Stop();
                }
                else
                {
                    Debug.LogWarning("AudioSource não encontrado!");
                }

                if (smokeEffect != null)
                {
                    smokeEffect.SetActive(false);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica se o hambúrguer entrou em contato com a chapa
        if (other.CompareTag("Fritadeira"))
        {
            isOnFryer = true;
            timer = 0f;  // Reseta o timer quando o hambúrguer entra na chapa
            if (audioSource != null)
            {
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("AudioSource não encontrado!");
            }
            if (smokeEffect != null)
            {
                smokeEffect.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Verifica se o hambúrguer saiu da chapa
        if (other.CompareTag("Fritadeira"))
        {
            isOnFryer = false;
            timer = 0f;  // Reseta o timer ao sair da chapa

            if (smokeEffect != null)
            {
                smokeEffect.SetActive(false);
            }
        }
    }
}

