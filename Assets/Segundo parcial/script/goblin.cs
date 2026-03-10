using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblin : MonoBehaviour
{

    public Rigidbody2D rbEnemy;
    public Transform[] PatrolPoint;
    private int PuntoActual;
    public bool isMooving;
    public int contadorDescansos;  
    public float speed;
    public bool isRight;

    private Vector2 leftDir;
    private Vector2 rightDir;
     public bool wasDamagedColor;
    public SpriteRenderer Sprite;

    void Start()
    {
        rbEnemy = GetComponent<Rigidbody2D>();  

         foreach(Transform goblin in PatrolPoint)
        {
            goblin.parent = null;
        }
    }

  
    void Update()
    {
        Transform target = PatrolPoint[PuntoActual];

        Vector2 direction = (target.position - transform.position).normalized;


        if(transform.position.x >= rightDir.x)
        { 
            isRight = false;
        }
        else if(transform.position.x <= leftDir.x)
        {
            isRight = true;
        }         
    
        if (isRight)
        {
            rbEnemy.linearVelocity = new Vector2(speed, rbEnemy.linearVelocity.y) ;
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        else
        {
            rbEnemy.linearVelocity = new Vector2(-speed, rbEnemy.linearVelocity.y);
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }

        if(Vector2.Distance(transform.position, target.position) <= 0.1f)
        {
            PuntoActual++;
            contadorDescansos++;

            if(PuntoActual >= PatrolPoint.Length)
            {
                PuntoActual = 0;
            }

            if (contadorDescansos >= 4)
            {
                StartCoroutine(CooldownEspera());
            }
        }         
    }

     IEnumerator CooldownEspera()
    {
        isMooving = false;

        yield return new WaitForSeconds(2f);

        contadorDescansos = 0;
        isMooving = true;
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
        }
    }

}