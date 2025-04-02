using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ataque : MonoBehaviour
{
    private Collider2D ataqueArea = default;

    public bool atacando = false;

    private float tempoDoAtaque = 0.7f;
    private float tempo = 0f;
    public event Action mudou;

    [Header("Tecla de Ataque")]
    public KeyCode BotaoAtk = KeyCode.F;
    // Factory method that generates a playable based on this asset
    private void Start()
    {
        ataqueArea = GetComponent<Collider2D>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(BotaoAtk) && !atacando)
        {
            Atk();
        }

        if (atacando)
        {
            tempo += Time.deltaTime;
            if (tempo >= tempoDoAtaque)
            {
                tempo = 0;
                atacando = false;
                mudou.Invoke();
            }
        }
        ataqueArea.enabled = atacando;
    }

    private void Atk()
    {
        atacando = true;
        mudou.Invoke();
    }

}
