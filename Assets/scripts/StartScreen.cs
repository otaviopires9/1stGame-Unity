using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public string nextSceneName = "CharacterSelection"; // Nome da cena para carregar

    void Update()
    {
        if (Input.anyKeyDown) // Se qualquer tecla for pressionada
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
