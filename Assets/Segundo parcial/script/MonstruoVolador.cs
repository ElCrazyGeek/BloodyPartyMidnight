using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volador : MonoBehaviour
{
    public const string Volando = "Vuelo";
    public const string MuerteVolador = "Muerte";
    public Rigidbody2D rbEnemy;
    public float speed;
    public Transform[] patrolPoint;
    private int puntoActual;
    public bool isMoving;
    public int contadorDescansos;
    private Vector2[] posiciones;

    public bool wasDamagedColor;
    public SpriteRenderer Sprite;

    public bool vivo = true;

    public int vida = 20;
    public bool muerto;
    private string currentState;
    public Animator aniVolador;
   
    void Start()
    {
        foreach(Transform tilin in patrolPoint)
        {
            tilin.parent = null;
        }
        vivo = true;
    }

  
    void Update()
    {
        Transform target = patrolPoint[puntoActual];

        Vector2 direction = (target.position - transform.position).normalized;
        if(vivo){ 
            ChangeAnimation(Volando);
        if (isMoving)
        {
            rbEnemy.linearVelocity = direction * speed;
        }
        else
        {
            rbEnemy.linearVelocity = Vector2.zero;
        }

        if(direction.x > 0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        else if(direction.x < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }

        if(Vector2.Distance(transform.position, target.position) <= 0.1f)
        {
            puntoActual++;
            contadorDescansos++;

            if(puntoActual >= patrolPoint.Length)
            {
                puntoActual = 0;
            }

            if (contadorDescansos >= 4)
            {
                StartCoroutine(CooldownEspera());
            }
        }
        }
        
    }

    
    IEnumerator CooldownEspera()
    {
        isMoving = false;

        yield return new WaitForSeconds(2f);

        contadorDescansos = 0;
        isMoving = true;
    }
    IEnumerator Damage_Rotine()
    {
        wasDamagedColor = true;
        Sprite.color = Color.red;

        yield return new WaitForSeconds(0.15f); 

        Sprite.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("sword"))
        {
            if (!wasDamagedColor)
            {
                StartCoroutine(Damage_Rotine());
            }
            vida -= 5;
        }

        if(vida <= 0)
        {
            vivo = false; 
            muerto = true;
        }

        if (!vivo)
        {
            speed = 0;
            muerte();
        }
    }


    void muerte()
    {
        ChangeAnimation(MuerteVolador);
        Destroy(gameObject,1.5F);
        
    }

     public void ChangeAnimation(string newState)
    {
        if(newState == currentState) return;
        currentState = newState;
        aniVolador.Play(currentState);
    }

}