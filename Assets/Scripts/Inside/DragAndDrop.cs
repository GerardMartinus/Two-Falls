using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool isDragging = false;
    private Vector2 originalPosition;
    private static bool isMousePressed = false; // Variável estática para o estado do mouse
    public bool isOriginal = true; // Identifica se o objeto é o original

    void Start()
    {
        // Armazena a posição original do objeto
        originalPosition = transform.position;
    }

    void OnMouseDown()
    {
        // Apenas o objeto original pode criar um clone
        if (isOriginal)
        {
            // Cria um clone do objeto e inicia o arraste nele
            GameObject clone = Instantiate(gameObject, originalPosition, Quaternion.identity);
            DragAndDrop cloneScript = clone.GetComponent<DragAndDrop>();
            cloneScript.isOriginal = false; // Define que o clone não é o original
            cloneScript.isDragging = true; // Inicia o arraste no clone
            isMousePressed = true; // Define o mouse como pressionado para o clone
        }
        else
        {
            // Se é um clone, apenas ativa o arraste
            isDragging = true;
            isMousePressed = true; // Define o mouse como pressionado
        }
    }

    public void OnMouseUp()
    {
        // Para o arraste ao soltar o botão do mouse
        isMousePressed = false; // Define o mouse como liberado
        isDragging = false;
    }

    void Update()
    {
        if (isDragging && isMousePressed)
        {
            // Move o objeto arrastado com o cursor do mouse
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
        }
        else if (!isMousePressed)
        {
            // Para o movimento quando o mouse é liberado
            isDragging = false;
        }
    }
}
