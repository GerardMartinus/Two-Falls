using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public int contagemDisperdicio;
    void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto é arrastável (opcional: pode verificar por tag, camada ou componente)
        if (other.CompareTag("Tomate") ||
            other.CompareTag("Queijo") ||
            other.CompareTag("Cebola") ||
            other.CompareTag("Alface") ||
            other.CompareTag("Pao") ||
            other.CompareTag("Batata") ||
            other.CompareTag("Hamburguer") ||
            other.CompareTag("Hamburguer_Frito") ||
            other.CompareTag("Batata_Frita"))
        {
            // Destroi o objeto ao entrar na lixeira
            Destroy(other.gameObject);

            contagemDisperdicio += 1;
        }
    }
}