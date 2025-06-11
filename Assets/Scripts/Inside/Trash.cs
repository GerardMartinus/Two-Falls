using UnityEngine;

public class TrashCan : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto é arrastável (opcional: pode verificar por tag, camada ou componente)
        if (collision.CompareTag("Tomate") ||
            collision.CompareTag("Queijo") ||
            collision.CompareTag("Cebola") ||
            collision.CompareTag("Alface") ||
            collision.CompareTag("Pao") ||
            collision.CompareTag("Batata") ||
            collision.CompareTag("Hamburguer") ||
            collision.CompareTag("Hamburguer_Frito") ||
            collision.CompareTag("Batata_Frito"))
        {
        // Destroi o objeto ao entrar na lixeira
        Destroy(collision.gameObject);
        }
    }
}