using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Vida : MonoBehaviour
{
    // Factory method that generates a playable based on this asset
    [SerializeField] public int vida = 100;


    public GameObject finalJogo;

    public bool tomouDano = false;
    public event Action OnVidaChange;

    private void Start()
    {
        finalJogo.SetActive(false);
    }

    void Update()
    {

    }

    public void Dano(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("sem Damage negativo");
        }
        this.vida -= amount;
        OnVidaChange.Invoke();
        StartCoroutine(AnimaçãoDanoTime());
        if (vida <= 0)
        {
            finalJogo.SetActive(true);
            Die();
        }


    }

    private void Die()
    {
        if (vida <= 0)
        {
            // removendo scrispt ao morrer
            GetComponentInChildren<Mov>().enabled = false;
            GetComponentInChildren<Ataque>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dano")
        {
            Debug.Log("Tomou dano");
            Dano(5);
        }
    }
    IEnumerator AnimaçãoDanoTime()
    {
        tomouDano = true;
        yield return new WaitForSeconds(1); // Converte ms para segundos
        tomouDano = false;
    }
}
