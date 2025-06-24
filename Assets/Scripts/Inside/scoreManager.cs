using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public TrashCan disperdicio;

    public int clientesSatisfeitos = 0;
    public int pedidosEntregues = 0;
    public int ingredientesDesperdicados = 0;

    private int melhorScoreEstacao = 0;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantém o ScoreManager entre cenas
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Evita duplicação em cenas futuras
        }
    }

    void Update()
    {
        ingredientesDesperdicados = disperdicio.contagemDisperdicio;
    }
 
    public void RegistrarClienteSatisfeito()
    {
        clientesSatisfeitos++;
    }

    public void RegistrarDescarte()
    {
        ingredientesDesperdicados++;
        Debug.Log($"🗑️ Desperdício registrado! Total: {ingredientesDesperdicados}");

        // Aqui pode adicionar efeitos sonoros, animações, etc.
    }

    public void RegistrarPedidoEntregue()
    {
        pedidosEntregues++;
    }

    public void RegistrarDesperdicio()
    {
        ingredientesDesperdicados++;
    }

    public int CalcularScoreDoDia()
    {
        return (clientesSatisfeitos * 10)
            + (pedidosEntregues * 5)
            - (ingredientesDesperdicados * 3);
    }

    public void VerificarMelhorScore()
    {
        int scoreAtual = CalcularScoreDoDia();
        if (scoreAtual > melhorScoreEstacao)
        {
            melhorScoreEstacao = scoreAtual;
        }
    }

    public int GetMelhorScoreDaEstacao()
    {
        return melhorScoreEstacao;
    }

    public void ResetarScoreDiario()
    {
        clientesSatisfeitos = 0;
        pedidosEntregues = 0;
        ingredientesDesperdicados = 0;
    }

    public void ResetarMelhorScore()
    {
        melhorScoreEstacao = 0;
    }
}
