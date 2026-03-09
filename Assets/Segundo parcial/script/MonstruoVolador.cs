using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volador : MonoBehaviour
{
    public Rigidbody2D rbEnemy;
    public float speed;
    public Transform[] patrolPoint;
    private int puntoActual;

    public bool isMoving;
    public int contadorDescansos;
    private Vector2[] posiciones;
   
    void Start()
    {
        foreach(Transform tilin in patrolPoint)
        {
            tilin.parent = null;
        }
    }

  
    void Update()
    {
        Transform target = patrolPoint[puntoActual];

        Vector2 direction = (target.position - transform.position).normalized;

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

            if (contadorDescansos >= 3)
            {
                StartCoroutine(CooldownEspera());
            }
        }
    }

    
    IEnumerator CooldownEspera()
    {
        isMoving = false;

        yield return new WaitForSeconds(1f);

        contadorDescansos = 0;
        isMoving = true;
    }
    
}