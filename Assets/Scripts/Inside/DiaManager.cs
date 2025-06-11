using System;
using TMPro;
using UnityEngine;

public class DiaManager : MonoBehaviour
{
    public static event Action OnDiaTerminado;
    public static event Action<string, int> OnTrocaEstacao;

    [Header("Tempo por Dia")]
    public float tempoPorDia = 120f;
    private float tempoRestante;

    [Header("Estações")]
    public string[] estacoes = { "Outono", "Inverno", "Primavera", "Verão", "Outono Final" };
    public int diasPorEstacao = 10;

    [Header("Referências de UI")]
    public TextMeshProUGUI textoTimer;
    public TextMeshProUGUI textoDia;
    public TextMeshProUGUI textoEstacao;

    private int diaAtual = 1;
    private int estacaoAtual = 0;
    private bool diaAtivo = false;

    private void Start()
    {
        IniciarJogo();
    }

    private void Update()
    {
        if (!diaAtivo || Time.timeScale == 0f)
            return;

        tempoRestante -= Time.deltaTime;

        if (tempoRestante <= 0f)
        {
            FimDoDia();
        }

        AtualizarUI();
    }

    public void IniciarJogo()
    {
        tempoRestante = tempoPorDia;
        diaAtivo = true;
        Time.timeScale = 1f;
    }

    public void ContinuarDia()
    {
        tempoRestante = tempoPorDia;
        diaAtivo = true;
        Time.timeScale = 1f;
        AtualizarUI();
    }

    void FimDoDia()
    {
        diaAtivo = false;
        Time.timeScale = 0f;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.VerificarMelhorScore();
        }

        OnDiaTerminado?.Invoke();

        if (diaAtual % diasPorEstacao == 0 && estacaoAtual < estacoes.Length - 1)
        {
            estacaoAtual++;
            OnTrocaEstacao?.Invoke(estacoes[estacaoAtual], estacaoAtual);
        }

        diaAtual++;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetarScoreDiario();
        }

        DestruirTodosIngredientes();
    }

    void AtualizarUI()
    {
        if (textoTimer != null)
        {
            float tempoDecorrido = tempoPorDia - tempoRestante;
            float totalMinutos = Mathf.Lerp(6 * 60f, 17 * 60f, tempoDecorrido / tempoPorDia);

            int horas = Mathf.FloorToInt(totalMinutos / 60f);
            int minutos = Mathf.FloorToInt(totalMinutos % 60f);
            textoTimer.text = $"Hora: {horas:00}:{minutos:00}";
        }

        if (textoDia != null)
            textoDia.text = $"Dia: {diaAtual}";

        if (textoEstacao != null)
            textoEstacao.text = $"Estação: {estacoes[estacaoAtual]}";
    }

    void DestruirTodosIngredientes()
    {
        string[] tagsIngredientes =
        {
            "Pao",
            "Hamburguer_Frito",
            "Hamburguer",
            "Batata_Frita",
            "Batata",
            "Cebola",
            "Tomate",
            "Alface",
            "Queijo",
        };

        foreach (string tag in tagsIngredientes)
        {
            GameObject[] objetos = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objetos)
            {
                if (obj.name.Contains("(Clone)"))
                {
                    Destroy(obj);
                }
            }
        }
    }
}
