using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mov : MonoBehaviour
{
    public const int PulosMaximo = 1;
    public float speed = 10f;
    public float forcaPulo = 100f;
    public float gravidade = 1f;
  
    public GameObject oponente;
    public int PulosTotal = PulosMaximo;

    [Header("Botões de Movimento")]
    public KeyCode BotaoPulo = KeyCode.W;
    public KeyCode BotaoDescer = KeyCode.S;
    public KeyCode BotaoEsquerda = KeyCode.A;
    public KeyCode BotaoDireita = KeyCode.D;


    Vector2 moveNow;
    Vector2 knockback = Vector2.zero;
    Vector2 knockbackVel;

    Vector2 jumpForce;
    Vector2 jumpForceVel;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        moveNow = Vector2.zero;
        if (knockback.sqrMagnitude > 0.1)
        {
            knockback = Vector2.SmoothDamp(knockback, Vector2.zero, ref knockbackVel, 0.1f);
            Debug.LogFormat("knockback {0}", knockback);
        }
        else
        {
            Movimenta();
            Pulo();
        }
        jumpForce = Vector2.SmoothDamp(jumpForce, Vector2.zero, ref jumpForceVel, 0.2f);
        Vector2 gravity = Vector2.down * 5;
        rb.MovePosition(rb.position + (moveNow + gravity + knockback + jumpForce) * Time.fixedDeltaTime);
        // Flip();

    }

    void Movimenta()
    {
        if (Input.GetKey(BotaoEsquerda))
        {
            Vector2 mov = new Vector2(-1, 0);
            moveNow += mov * speed;
           
        }

        else if (Input.GetKey(BotaoDireita))
        {
            Vector2 mov = new Vector2(1, 0);
            moveNow += mov * speed;
            
        }
    
    }

    public void DoKnockback(Vector2 forca)
    {
        knockback = forca;
    }

    void Pulo()
    {
        //      Debug.LogFormat("Oi estou pulado {0} {1}", PulosTotal, BotaoPulo);
        if (Input.GetKey(BotaoPulo))
        {
            if (PulosTotal > 0)
            {
                Vector2 pulo = new Vector2(0, forcaPulo);
                moveNow += pulo * speed;
                jumpForce = Vector2.up * forcaPulo;
                PulosTotal--;

            }
        }
    }
    //void Down()
    //{
    //    if (Input.GetKey(BotaoDescer))
    //    {
    //        Vector2 down = new Vector2(0, -forcaPulo);
    //        moveNow += down * speed;
    //    }
    //}

    void Flip()
    {
        if (transform.position.x - oponente.transform.position.x < 0)
        {
            var SpriteFilhos = gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (var Sprite in SpriteFilhos)
            {
                Sprite.flipX = true;
            }
        }
        else
        {
            var SpriteFilhos = gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (var Sprite in SpriteFilhos)
            {
                Sprite.flipX = false;
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Layer")
        {
            PulosTotal = PulosMaximo;
        }
    }
   

}
