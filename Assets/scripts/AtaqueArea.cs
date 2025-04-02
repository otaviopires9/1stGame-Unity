using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueArea : MonoBehaviour
{
    private int dano = 0;
    public Vector2 forca = Vector2.right;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Vida>() != null)
        {
            Vida hp = collider.GetComponent<Vida>();
            hp.Dano(dano);
            Mov mov = collider.GetComponent<Mov>();
            mov.DoKnockback(forca);
        }
    }
}
