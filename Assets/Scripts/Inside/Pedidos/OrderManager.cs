using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class IngredientIcon
{
    public string ingredientTag; // Tag do ingrediente
    public Sprite icon; // Ícone do ingrediente
}

public class OrderManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI orderText;
    public GameObject ingredientIconPrefab; // Prefab do ícone que será usado para mostrar os ingredientes
    public Transform ingredientIconsParent; // Objeto onde os ícones serão criados

    [Header("Ingredient Icons")]
    public List<IngredientIcon> ingredientIcons; // Lista de ícones para cada ingrediente

    [Header("Layout Settings")]
    public float iconSpacing = 100f; // Espaçamento entre os ícones
    public Vector2 iconSize = new Vector2(64, 64); // Tamanho dos ícones
    public event System.Action OnNewOrder;

    [Header("NPC Settings")]
    public Transform npcSpawnPoint;
    public List<GameObject> headPrefabs;
    public List<GameObject> hairPrefabs;
    private GameObject currentNPC;

    private List<GameObject> currentIcons = new List<GameObject>();
    public List<string> currentOrderIngredients { get; private set; } = new List<string>();

    private string[] possibleIngredients = new string[]
    {
        "Batata_Frita",
        "Cebola",
        "Tomate",
        "Alface",
        "Queijo",
    };

    void Start()
    {
        orderText.text = "Pedido!";
        if (ingredientIconPrefab == null || ingredientIconsParent == null)
        {
            Debug.LogError("Erro: Configure o prefab do ícone e o parent no Inspector!");
            return;
        }

        GenerateNewOrder();
        OnNewOrder?.Invoke();
        orderText.text = "Pedido!";
    }

    public void GenerateNewOrder()
    {
        // Limpa os ícones antigos
        ClearCurrentIcons();

        // Limpa a lista de ingredientes atual
        currentOrderIngredients.Clear();

        // Adiciona ingredientes obrigatórios
        currentOrderIngredients.Add("Pao");
        currentOrderIngredients.Add("Hamburguer_Frito");

        // Adiciona 1-3 ingredientes aleatórios
        int extraIngredients = Random.Range(1, 4);

        for (int i = 0; i < extraIngredients; i++)
        {
            string randomIngredient = possibleIngredients[
                Random.Range(0, possibleIngredients.Length)
            ];
            if (!currentOrderIngredients.Contains(randomIngredient))
            {
                currentOrderIngredients.Add(randomIngredient);
            }
        }

        UpdateOrderUI();

        // Gera o NPC correspondente ao novo pedido
        GenerateNPC();
    }

    void UpdateOrderUI()
    {
        orderText.text = "Novo Pedido!";
        OnNewOrder?.Invoke();
        // Cria os ícones para cada ingrediente
        for (int i = 0; i < currentOrderIngredients.Count; i++)
        {
            string ingredient = currentOrderIngredients[i];

            // Encontra o ícone correspondente
            IngredientIcon ingredientIcon = ingredientIcons.Find(icon =>
                icon.ingredientTag == ingredient
            );

            if (ingredientIcon != null)
            {
                // Cria o ícone
                GameObject newIcon = Instantiate(ingredientIconPrefab, ingredientIconsParent);
                Image iconImage = newIcon.GetComponent<Image>();

                // Configura o ícone
                iconImage.sprite = ingredientIcon.icon;
                iconImage.preserveAspect = true;

                // Configura o tamanho
                RectTransform rect = newIcon.GetComponent<RectTransform>();
                rect.sizeDelta = iconSize;

                // Posiciona o ícone
                float xPos = (i - (currentOrderIngredients.Count - 1) / 2f) * iconSpacing;
                rect.anchoredPosition = new Vector2(xPos, 0);

                // Adiciona à lista de ícones atuais
                currentIcons.Add(newIcon);
            }
        }
    }

    void ClearCurrentIcons()
    {
        foreach (GameObject icon in currentIcons)
        {
            Destroy(icon);
        }
        currentIcons.Clear();
    }

    void GenerateNPC()
    {
        // Destrói NPC anterior (se existir)
        if (currentNPC != null)
        {
            Destroy(currentNPC);
        }

        // Instancia cabeça aleatória
        GameObject randomHead = headPrefabs[Random.Range(0, headPrefabs.Count)];
        currentNPC = Instantiate(randomHead, npcSpawnPoint.position, Quaternion.identity);

        // Instancia cabelo aleatório como filho da cabeça
        GameObject randomHair = hairPrefabs[Random.Range(0, hairPrefabs.Count)];
        GameObject hairInstance = Instantiate(randomHair, currentNPC.transform);

        // Ajustar posição e rotação do cabelo (ajuste se precisar)
        hairInstance.transform.localPosition = new Vector3(0, 0, -0.01f); // Ajuste para sprites 2D
        hairInstance.transform.localRotation = Quaternion.identity;

        StartCoroutine(FadeInNPC(currentNPC));
    }

    IEnumerator FadeInNPC(GameObject npc)
    {
        if (npc == null)
            yield break;

        CanvasGroup canvasGroup = npc.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = npc.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f;
        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (npc == null || canvasGroup == null)
                yield break;

            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / duration);
            yield return null;
        }

        if (npc != null && canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
        }
    }
}
