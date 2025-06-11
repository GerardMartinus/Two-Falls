using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [System.Serializable]
    public class DialogLine
    {
        public string speaker; // Quem está falando
        public string message; // O que está sendo falado
        public float displayTime = 3f;
    }

    [System.Serializable]
    public class DialogSequence
    {
        public List<DialogLine> lines = new List<DialogLine>();
    }

    private List<DialogSequence> allDialogSequences = new List<DialogSequence>();
    private List<DialogSequence> unusedSequences;
    private bool isDisplayingDialog = false;
    private float currentDisplayTime;
    private DialogSequence currentSequence;
    private int currentLineIndex = 0;

    [Header("Configurações")]
    [SerializeField]
    private float timeBetweenDialogs = 1f;

    [SerializeField]
    private DialogUI dialogUI;

    [Header("Referências")]
    [SerializeField]
    private OrderManager orderManager;

    void Start()
    {
        InitializeDialogs();
        ResetDialogList();

        if (orderManager != null)
        {
            orderManager.OnNewOrder += HandleNewOrder;
        }
        else
        {
            Debug.LogWarning("OrderManager não está referenciado no DialogManager!");
        }
    }

    private void InitializeDialogs()
    {
        // Dialogo-1
        var sequence1 = new DialogSequence();
        sequence1.lines.Add(
            new DialogLine
            {
                speaker = "Cliente",
                message = "Oi, você é novo por aqui?",
                displayTime = 3f,
            }
        );
        sequence1.lines.Add(
            new DialogLine
            {
                speaker = "Neto",
                message = "Sou. Estou ajudando meu avô por um tempo.",
                displayTime = 3f,
            }
        );
        sequence1.lines.Add(
            new DialogLine
            {
                speaker = "Cliente",
                message = "Que estranho, normalmente o seu avô lida com as coisas aqui sozinho.",
                displayTime = 3f,
            }
        );
        sequence1.lines.Add(
            new DialogLine
            {
                speaker = "Neto",
                message =
                    "Ah, então você é um cliente antigo do meu avô. Ele está passando por um momento difícil, então precisou de ajuda.",
                displayTime = 3f,
            }
        );
        sequence1.lines.Add(
            new DialogLine
            {
                speaker = "Cliente",
                message = "Que bom que seu avô pode contar com você para ajudar ele. Boa sorte!",
                displayTime = 4f,
            }
        );
        allDialogSequences.Add(sequence1);

        // Dialogo-2
        var sequence2 = new DialogSequence();
        sequence2.lines.Add(
            new DialogLine
            {
                speaker = "Cliente",
                message = "Oi rapaz, hoje quem está no comando?",
                displayTime = 3f,
            }
        );
        sequence2.lines.Add(
            new DialogLine
            {
                speaker = "Neto",
                message = "Oi! Hoje sou eu novamente, meu avô ainda está se recuperando.",
                displayTime = 3f,
            }
        );
        sequence2.lines.Add(
            new DialogLine
            {
                speaker = "Cliente",
                message = "Ele está melhorando?",
                displayTime = 3f,
            }
        );
        sequence2.lines.Add(
            new DialogLine
            {
                speaker = "Neto",
                message = "Ele tem dias bons e ruins, mas estamos esperançosos.",
                displayTime = 3f,
            }
        );
        sequence2.lines.Add(
            new DialogLine
            {
                speaker = "Cliente",
                message = "Vou torcer por ele! Boa sorte aqui.",
                displayTime = 4f,
            }
        );
        allDialogSequences.Add(sequence2);

        // Dialogo-3
        var sequence3 = new DialogSequence();
        sequence3.lines.Add(
            new DialogLine
            {
                speaker = "Cliente",
                message = "oi garoto, como está seu avô?",
                displayTime = 3f,
            }
        );
        sequence3.lines.Add(
            new DialogLine
            {
                speaker = "Neto",
                message = "ele está um pouco melhor hoje, obrigado por perguntar",
                displayTime = 3f,
            }
        );
        sequence3.lines.Add(
            new DialogLine
            {
                speaker = "Cliente",
                message = "que bom ouvir isso. Mande lembranças para ele",
                displayTime = 4f,
            }
        );
        allDialogSequences.Add(sequence3);

        // Dialogo-4
        var sequence4 = new DialogSequence();
        sequence4.lines.Add(
            new DialogLine
            {
                speaker = "Cliente",
                message = "parece que seu avô não está aqui hoje. Está tudo bem?",
                displayTime = 3f,
            }
        );
        sequence4.lines.Add(
            new DialogLine
            {
                speaker = "Neto",
                message = "ele teve uma recaída e está descansando em casa hoje",
                displayTime = 3f,
            }
        );
        sequence4.lines.Add(
            new DialogLine
            {
                speaker = "Cliente",
                message = "lamento ouvir isso. Espero que ele melhore logo",
                displayTime = 4f,
            }
        );
        allDialogSequences.Add(sequence4);

        // Dialogo-5
        var sequence5 = new DialogSequence();
        sequence5.lines.Add(
            new DialogLine
            {
                speaker = "Cliente",
                message = "como está seu avô hoje?",
                displayTime = 3f,
            }
        );
        sequence5.lines.Add(
            new DialogLine
            {
                speaker = "Neto",
                message = "ele tem dias bons e ruins. Hoje ele está descansando bastante",
                displayTime = 3f,
            }
        );
        sequence5.lines.Add(
            new DialogLine
            {
                speaker = "Cliente",
                message = "fico torcendo pela recuperação dele",
                displayTime = 4f,
            }
        );
        allDialogSequences.Add(sequence5);

        // Dialogo-6
        var sequence6 = new DialogSequence();
        sequence6.lines.Add(
            new DialogLine
            {
                speaker = "Cliente",
                message = "e ai, tudo bem?",
                displayTime = 3f,
            }
        );
        sequence6.lines.Add(
            new DialogLine
            {
                speaker = "Neto",
                message = "tudo bem, obrigado. Meu avô está estável hoje",
                displayTime = 3f,
            }
        );
        sequence6.lines.Add(
            new DialogLine
            {
                speaker = "Cliente",
                message = "que bom ouvir isso. Força para vocês",
                displayTime = 4f,
            }
        );
        allDialogSequences.Add(sequence6);

        // Dialogo-7
        var sequence7 = new DialogSequence();
        sequence7.lines.Add(
            new DialogLine
            {
                speaker = "Cliente",
                message = "alguma novidade sobre a saúde do seu avô?",
                displayTime = 3f,
            }
        );
        sequence7.lines.Add(
            new DialogLine
            {
                speaker = "Neto",
                message = "infelizmente, ele teve uma complicação e está no hospital agora",
                displayTime = 3f,
            }
        );
        sequence7.lines.Add(
            new DialogLine
            {
                speaker = "Cliente",
                message = "sinto muito por isso. Espero que ele receba o melhor cuidado possível",
                displayTime = 4f,
            }
        );
        allDialogSequences.Add(sequence7);

        // Dialogo-8
        var sequence8 = new DialogSequence();
        sequence8.lines.Add(
            new DialogLine
            {
                speaker = "Cliente",
                message = "como você está? E seu avô?",
                displayTime = 3f,
            }
        );
        sequence8.lines.Add(
            new DialogLine
            {
                speaker = "Neto",
                message = "estou preocupado, mas estamos tentando manter a esperança",
                displayTime = 3f,
            }
        );
        sequence8.lines.Add(
            new DialogLine
            {
                speaker = "Cliente",
                message = "estou aqui se precisar de qualquer coisa. Força, rapaz",
                displayTime = 4f,
            }
        );
        allDialogSequences.Add(sequence8);

        // Diálogo-9
        var sequence9 = new DialogSequence();
        sequence9.lines.Add(
            new DialogLine
            {
                speaker = "Cliente",
                message = "estou torcendo muito pela recuperação do seu avô. Alguma notícia boa?",
                displayTime = 3f,
            }
        );
        sequence9.lines.Add(
            new DialogLine
            {
                speaker = "Neto",
                message = "ele teve uma pequena melhora hoje, obrigado por perguntar",
                displayTime = 3f,
            }
        );
        sequence9.lines.Add(
            new DialogLine
            {
                speaker = "Cliente",
                message = "que bom ouvir isso! Mande forças para ele",
                displayTime = 4f,
            }
        );
        allDialogSequences.Add(sequence9);

        // Diálogo-10
        var sequence10 = new DialogSequence();
        sequence10.lines.Add(
            new DialogLine
            {
                speaker = "Cliente",
                message = "como está seu avô hoje?",
                displayTime = 3f,
            }
        );
        sequence10.lines.Add(
            new DialogLine
            {
                speaker = "Neto",
                message = "ele está descansando. Cada dia é uma batalha",
                displayTime = 3f,
            }
        );
        sequence10.lines.Add(
            new DialogLine
            {
                speaker = "Cliente",
                message = "continuem fortes. Estamos todos torcendo por ele",
                displayTime = 4f,
            }
        );
        allDialogSequences.Add(sequence10);
        //Adicionar quantos dialogos quiser
    }

    private void ResetDialogList()
    {
        unusedSequences = new List<DialogSequence>(allDialogSequences);
    }

    private void HandleNewOrder()
    {
        if (!isDisplayingDialog)
        {
            StartNewDialogSequence();
        }
    }

    private void StartNewDialogSequence()
    {
        if (unusedSequences.Count == 0)
        {
            ResetDialogList();
        }

        int randomIndex = Random.Range(0, unusedSequences.Count);
        currentSequence = unusedSequences[randomIndex];
        unusedSequences.RemoveAt(randomIndex);

        currentLineIndex = 0;
        DisplayCurrentLine();
    }

    private void DisplayCurrentLine()
    {
        if (
            dialogUI != null
            && currentSequence != null
            && currentLineIndex < currentSequence.lines.Count
        )
        {
            DialogLine currentLine = currentSequence.lines[currentLineIndex];
            dialogUI.ShowDialog($"{currentLine.speaker}: {currentLine.message}");

            isDisplayingDialog = true;
            currentDisplayTime = currentLine.displayTime;
        }
    }

    void Update()
    {
        if (isDisplayingDialog)
        {
            currentDisplayTime -= Time.deltaTime;

            if (currentDisplayTime <= 0)
            {
                currentLineIndex++;

                if (currentLineIndex < currentSequence.lines.Count)
                {
                    // Ainda tem linhas para mostrar
                    DisplayCurrentLine();
                }
                else
                {
                    // Terminou a sequência
                    isDisplayingDialog = false;
                    dialogUI?.HideDialog();
                    currentLineIndex = 0;
                }
            }
        }
    }

    public void StartSeasonDialog(int seasonIndex)
    {
        if (unusedSequences.Count > 0)
        {
            // Escolha um diálogo aleatório
            int randomIndex = Random.Range(0, unusedSequences.Count);
            currentSequence = unusedSequences[randomIndex];
            currentLineIndex = 0;

            // Remove o diálogo para evitar repetição
            unusedSequences.RemoveAt(randomIndex);

            DisplayCurrentLine(); // Exibe o diálogo
        }
        else
        {
            Debug.Log("Todos os diálogos já foram usados. Nenhum novo diálogo será iniciado.");
        }
    }

    public void TestNextDialog()
    {
        if (!isDisplayingDialog)
        {
            StartNewDialogSequence();
        }
    }
}
