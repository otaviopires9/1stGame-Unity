using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private bool isWalking = false;

    private Mov mov;
    private Ataque atk;
    private Vida vd;
    void Start()
    {
        mov = GetComponentInParent<Mov>();
        vd = GetComponentInParent<Vida>();
        atk = GetComponentInChildren<Ataque>();

        atk.mudou += QuandoMudarAtk;
            
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Detecta se o jogador está pressionando uma tecla de movimento
        isWalking = Input.GetKey(mov.BotaoEsquerda) || Input.GetKey(mov.BotaoDireita);


        if(isWalking)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        

        if (vd.tomouDano)
        {
            animator.SetBool("TomarDano", true);
        }
        else
        {
            animator.SetBool("TomarDano", false);
        }
        // Atualiza a animação de dano
       // animator.SetBool("TomarDano", vd.tomouDano);
    }

    void QuandoMudarAtk()
    {
        animator.SetBool("Bater", atk.atacando);
        if (atk.atacando == true)
        {
            FindFirstObjectByType<MusicManager>().BaterSound();
        }
    }
}

