using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource backgroundMusic1; // Primeiro �udio de fundo
    public AudioSource backgroundMusic2; // Segundo �udio de fundo
    public AudioSource selectionSound;   // Som ao selecionar personagem
    public AudioSource baterSound;

    void Start()
    {
        // Garante que as m�sicas de fundo come�am a tocar
        if (backgroundMusic1 != null)
        {
            backgroundMusic1.loop = true;
            backgroundMusic1.Play();
        }

        if (backgroundMusic2 != null)
        {
            backgroundMusic2.loop = true;
            backgroundMusic2.Play();
        }
    }

    // M�todo para tocar o som ao selecionar personagem
    public void PlaySelectionSound()
    {
        if (selectionSound != null)
        {
            selectionSound.Play();
        }
        else
        {
            Debug.Log("Audio n�o encontrado");
        }
    }

    public void BaterSound()
    {
        if (baterSound != null)
        {
            baterSound.Play();
        }
        else
        {
            Debug.Log("Audio n�o encontrado");
        }
    }
}
