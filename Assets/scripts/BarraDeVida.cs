using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BarraDeVida : MonoBehaviour
{
    public Slider VidaBarra;

    Vida jogador;
    public GameObject lugarPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jogador = lugarPlayer.GetComponentInChildren<Vida>();
        jogador.OnVidaChange += PerdeVida;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PerdeVida()
    {
        VidaBarra.value = jogador.vida;
    }
}
