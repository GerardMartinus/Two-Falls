using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Painel Único")]
    public GameObject painelResumo;
    public TextMeshProUGUI textoResumo;
    public Button botaoContinuar;
    public GameObject fundoEscuro;

    private void OnEnable()
    {
        DiaManager.OnDiaTerminado += MostrarResumoDoDia;
        DiaManager.OnTrocaEstacao += MostrarMelhorScore;
        botaoContinuar.onClick.AddListener(FecharPainel);
    }

    private void OnDisable()
    {
        DiaManager.OnDiaTerminado -= MostrarResumoDoDia;
        DiaManager.OnTrocaEstacao -= MostrarMelhorScore;
        botaoContinuar.onClick.RemoveListener(FecharPainel);
    }

    void Start()
    {
        painelResumo?.SetActive(false);
        fundoEscuro?.SetActive(false);
    }

    void MostrarResumoDoDia()
    {
        if (ScoreManager.Instance == null)
        {
            Debug.LogError("ScoreManager não encontrado!");
            return;
        }

        int score = ScoreManager.Instance.CalcularScoreDoDia();

        textoResumo.text =
            $"Fim do Dia!\n\n"
            + $"Clientes satisfeitos: {ScoreManager.Instance.clientesSatisfeitos}\n"
            + $"Pedidos entregues: {ScoreManager.Instance.pedidosEntregues}\n"
            + $"Desperdício: {ScoreManager.Instance.ingredientesDesperdicados}\n"
            + $"Score do dia: {score}";

        fundoEscuro?.SetActive(true);
        painelResumo?.SetActive(true);
        Time.timeScale = 0f;
    }

    void MostrarMelhorScore(string nomeEstacao, int index)
    {
        if (ScoreManager.Instance == null)
        {
            Debug.LogError("ScoreManager não encontrado!");
            return;
        }

        int melhorScore = ScoreManager.Instance.GetMelhorScoreDaEstacao();

        textoResumo.text = $"Fim da estação: {nomeEstacao}\n\nMelhor Score: {melhorScore}";

        fundoEscuro?.SetActive(true);
        painelResumo?.SetActive(true);
        Time.timeScale = 0f;

        ScoreManager.Instance.ResetarMelhorScore();
    }

    void FecharPainel()
    {
        fundoEscuro?.SetActive(false);
        painelResumo?.SetActive(false);
        Time.timeScale = 1f;

        FindObjectOfType<DiaManager>()?.ContinuarDia();
        FindObjectOfType<OrderManager>()?.GenerateNewOrder();
    }
}
