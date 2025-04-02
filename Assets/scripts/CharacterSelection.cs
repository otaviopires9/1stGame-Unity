using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public string[] characterNames; // Lista de nomes dos personagens

    public GameObject previewAleatorioP1; // Preview do personagem ALEATÓRIO para Player 1
    public GameObject previewAleatorioP2; // Preview do personagem ALEATÓRIO para Player 2
    public GameObject previewPersonagem1P1; // Preview do Personagem 1 para Player 1
    public GameObject previewPersonagem1P2; // Preview do Personagem 1 para Player 2

    public TMPro.TextMeshProUGUI nomePersonagemP1; // UI para mostrar o nome do personagem Player 1
    public TMPro.TextMeshProUGUI nomePersonagemP2; // UI para mostrar o nome do personagem Player 2


    public GameObject[] characters; // Lista de personagens disponíveis
    private int player1Index = 0;
    private int player2Index = 0;

    public Transform player1Selector; // UI indicador de seleção Player 1
    public Transform player2Selector; // UI indicador de seleção Player 2

    private bool player1Confirmed = false; // Flag para confirmação Player 1
    private bool player2Confirmed = false; // Flag para confirmação Player 2



    public string nomeDaCena;

    public void Start()
    {
        // Desativar todos os previews antes de ativar o correto
        previewAleatorioP1.SetActive(false);
        previewAleatorioP2.SetActive(false);
        previewPersonagem1P1.SetActive(false);
        previewPersonagem1P2.SetActive(false);

        player1Confirmed = false;
        player2Confirmed = false;

        // Definir ambos os jogadores para o personagem ALEATÓRIO (índice 1)
        player1Index = 0;
        player2Index = 0;

        // Atualizar os seletores e ativar os previews corretos
        ChangeCharacter(ref player1Index, 0, player1Selector, true);
        ChangeCharacter(ref player2Index, 0, player2Selector, false);
    }
    void Update()
    {
        if (!player1Confirmed)
        {
            if (Input.GetKeyDown(KeyCode.A))
                ChangeCharacter(ref player1Index, -1, player1Selector, true);
            if (Input.GetKeyDown(KeyCode.D))
                ChangeCharacter(ref player1Index, 1, player1Selector, true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                ConfirmCharacter(1);
                player1Selector.gameObject.SetActive(false);
            }
        }

        if (!player2Confirmed)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                ChangeCharacter(ref player2Index, -1, player2Selector, false);
            if (Input.GetKeyDown(KeyCode.RightArrow))
                ChangeCharacter(ref player2Index, 1, player2Selector, false);
            if (Input.GetKeyDown(KeyCode.M))
            {
                ConfirmCharacter(2);
                player2Selector.gameObject.SetActive(false);
            }
        }




        if (player1Confirmed == true && player2Confirmed == true)
        {
            PassarCena();
        }
    }

    void ChangeCharacter(ref int playerIndex, int direction, Transform selector, bool isPlayer1)
    {
        playerIndex += direction;

        if (playerIndex < 0) playerIndex = characters.Length - 1;
        if (playerIndex >= characters.Length) playerIndex = 0;

        // Atualizar posição do seletor
        selector.position = characters[playerIndex].transform.position;

        // Atualizar o nome do personagem na UI
        if (isPlayer1)
        {
            nomePersonagemP1.text = characters[playerIndex].name;

            // Desativar APENAS o preview do Player 1 antes de ativar o correto
            previewAleatorioP1.SetActive(false);
            previewPersonagem1P1.SetActive(false);

            // Ativar o preview correto para o Player 1
            if (characters[playerIndex].name == "ALEATORIO")
                previewAleatorioP1.SetActive(true);
            else if (characters[playerIndex].name == "BEM-TE-VI")
                previewPersonagem1P1.SetActive(true);
        }
        else
        {
            nomePersonagemP2.text = characters[playerIndex].name;

            // Desativar APENAS o preview do Player 2 antes de ativar o correto
            previewAleatorioP2.SetActive(false);
            previewPersonagem1P2.SetActive(false);

            // Ativar o preview correto para o Player 2
            if (characters[playerIndex].name == "ALEATORIO")
                previewAleatorioP2.SetActive(true);
            else if (characters[playerIndex].name == "BEM-TE-VI")
                previewPersonagem1P2.SetActive(true);
        }

        // Debug para acompanhar a seleção
        Debug.Log("Player " + (isPlayer1 ? "1" : "2") + " selecionou: " + characters[playerIndex].name);
    }






    void ConfirmCharacter(int player)
    {
        if (player == 1)
        {
            player1Confirmed = true;

            // Se Player 1 escolheu "ALEATÓRIO", escolhe outro personagem aleatório
            if (player1Index == 0)
            {
                player1Index = EscolherPersonagemAleatorio();
                previewAleatorioP1.SetActive(false);
                previewPersonagem1P1.SetActive(true);
            }

            FindFirstObjectByType<MusicManager>().PlaySelectionSound();
            Debug.Log("Player 1 escolheu: " + characters[player1Index].name);
        }
        else if (player == 2)
        {
            player2Confirmed = true;

            // Se Player 2 escolheu "ALEATÓRIO", escolhe outro personagem aleatório
            if (player2Index == 0)
            {
                player2Index = EscolherPersonagemAleatorio();
                previewAleatorioP2.SetActive(false);
                previewPersonagem1P2.SetActive(true);
            }

            FindFirstObjectByType<MusicManager>().PlaySelectionSound();


            Debug.Log("Player 2 escolheu: " + characters[player2Index].name);
        }
    }


    int EscolherPersonagemAleatorio()
    {
        int novoIndex;
        do
        {
            novoIndex = Random.Range(1, characters.Length); // Escolhe aleatório, ignorando o índice 0 (ALEATÓRIO)
        } while (novoIndex == 0); // Garante que não selecione "ALEATÓRIO" novamente

        return novoIndex;
    }


    public void PassarCena()
    {
        SceneManager.LoadScene(nomeDaCena);
    }
}
