using UnityEngine;
using UnityEngine.UI;

public class BurgerCooking : MonoBehaviour
{
    public Sprite cookedBurgerSprite; // O sprite do hambúrguer frito
    public string newTag;
    public GameObject smokeEffect; // Objeto de efeito de fumaça com animação
    private bool isOnGrill = false; // Verifica se está na chapa
    private float cookingTime = 10f; // Tempo necessário para fritar o hambúrguer
    private float timer = 0f; // Timer para a contagem regressiva
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    void Start()
    {
        // Obtém o SpriteRenderer do hambúrguer
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.tag = "Hamburguer";
        audioSource = GetComponent<AudioSource>();
        // Certifica de que o efeito de fumaça está desativado no início
        if (smokeEffect != null)
        {
            smokeEffect.SetActive(false);
        }
    }

    void Update()
    {
        // Se o hambúrguer estiver na chapa, inicia a contagem
        if (isOnGrill)
        {
            Debug.Log("Ta na grelha");
            timer += Time.deltaTime;
            if (timer >= cookingTime)
            {
                // Troca o sprite para o hambúrguer frito após o tempo de cozimento
                spriteRenderer.sprite = cookedBurgerSprite;
                isOnGrill = false; // Reseta para evitar a contagem extra
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
                    smokeEffect.SetActive(true);
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
        if (other.CompareTag("Chapa"))
        {
            isOnGrill = true;
            timer = 0f; // Reseta o timer quando o hambúrguer entra na chapa
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
        if (other.CompareTag("Chapa"))
        {
            isOnGrill = false;
            timer = 0f; // Reseta o timer ao sair da chapa

            if (smokeEffect != null)
            {
                smokeEffect.SetActive(false);
            }
        }
    }
}
