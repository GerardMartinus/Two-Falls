using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterManager : MonoBehaviour
{
    private OrderManager orderManager;
    private List<string> ingredientsOnCounter = new List<string>();

    [SerializeField]
    private GameObject pedidoCompletoPrefab; // Prefab do lanche completo

    [SerializeField]
    private Transform notasObject; // Objeto do bloco de notas

    [SerializeField]
    private float moveDuration = 1f; // Tempo que leva para o lanche chegar ao bloco de notas

    // Lista para guardar as referências físicas dos objetos no balcão
    private List<GameObject> ingredientObjects = new List<GameObject>();

    [Header("Debug")]
    [SerializeField]
    private bool showDebugInfo = true;

    void Start()
    {
        orderManager = FindObjectOfType<OrderManager>();

        if (orderManager == null)
        {
            Debug.LogError("Erro: OrderManager não encontrado na cena!");
            return;
        }

        if (GetComponent<Collider>() == null)
        {
            Debug.LogError("Erro: Adicione um Collider ao balcão!");
            return;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        string[] allIngredientTags = new string[]
        {
            "Pao",
            "Hamburguer_Frito",
            "Batata_Frita",
            "Cebola",
            "Tomate",
            "Alface",
            "Queijo",
        };

        foreach (string tag in allIngredientTags)
        {
            if (other.CompareTag(tag))
            {
                if (!ingredientsOnCounter.Contains(tag))
                {
                    ingredientsOnCounter.Add(tag);
                    // Guarda a referência do objeto físico
                    ingredientObjects.Add(other.gameObject);

                    if (showDebugInfo)
                        Debug.Log($"Ingrediente adicionado ao balcão: {tag}");

                    CheckOrder();
                }
                break;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        string[] allIngredientTags = new string[]
        {
            "Pao",
            "Hamburguer_Frito",
            "Batata_Frita",
            "Cebola",
            "Tomate",
            "Alface",
            "Queijo",
        };

        foreach (string tag in allIngredientTags)
        {
            if (other.CompareTag(tag))
            {
                int index = ingredientsOnCounter.IndexOf(tag);
                if (index != -1)
                {
                    ingredientsOnCounter.RemoveAt(index);
                    ingredientObjects.RemoveAt(index);

                    if (showDebugInfo)
                        Debug.Log($"Ingrediente removido do balcão: {tag}");
                }
                break;
            }
        }
    }

    void CheckOrder()
    {
        if (orderManager == null)
            return;

        bool allIngredientsPresent = true;

        foreach (string requiredIngredient in orderManager.currentOrderIngredients)
        {
            if (!ingredientsOnCounter.Contains(requiredIngredient))
            {
                allIngredientsPresent = false;
                break;
            }
        }

        if (allIngredientsPresent)
        {
            if (showDebugInfo)
                Debug.Log("Todos os ingredientes encontrados! Completando pedido...");

            CompleteOrder();
        }
    }

    void CompleteOrder()
    {
        // Remove fisicamente todos os ingredientes do balcão
        foreach (GameObject ingredient in ingredientObjects)
        {
            if (ingredient != null)
            {
                Destroy(ingredient);
            }
        }

        // Limpa as listas
        ingredientObjects.Clear();
        ingredientsOnCounter.Clear();

        // Instancia o sprite do lanche completo e o move para o bloco de notas
        if (pedidoCompletoPrefab != null && notasObject != null)
        {
            GameObject orderSprite = Instantiate(
                pedidoCompletoPrefab,
                transform.position,
                Quaternion.identity
            );
            StartCoroutine(MoveToNotes(orderSprite, notasObject.position));
        }

        if (showDebugInfo)
            Debug.Log("Pedido completado com sucesso! Ingredientes removidos.");

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.RegistrarClienteSatisfeito();
            ScoreManager.Instance.RegistrarPedidoEntregue();
        }
    }

    IEnumerator MoveToNotes(GameObject orderSprite, Vector3 targetPosition)
    {
        Vector3 startPosition = orderSprite.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            orderSprite.transform.position = Vector3.Lerp(
                startPosition,
                targetPosition,
                elapsedTime / moveDuration
            );
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        orderSprite.transform.position = targetPosition;

        Destroy(orderSprite);

        // Gera um novo pedido
        orderManager.GenerateNewOrder();
    }

    IEnumerator FadeOutAndDestroy(GameObject ingredient)
    {
        float duration = 100f;
        float elapsedTime = 0f;
        Vector3 originalScale = ingredient.transform.localScale;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newScale = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            ingredient.transform.localScale = originalScale * newScale;
            yield return null;
        }

        Destroy(ingredient);
    }
}
